DROP PROCEDURE IF EXISTS select_loan_details; 
DELIMITER //
create procedure select_loan_details()
BEGIN
select L.Loan_ID as 'ID'
,L.MicroProject_ID as 'رقم المشروع' 
,CONCAT(P.P_FirstName, ' ',P.P_FatherName, ' ', P.P_LastName) as 'المستفيد'
,L.Loan_DateTaken as 'تاريخ استلام القرض'
,L.Loan_Amount as 'القرض'
,Round( ( Loan_Amount / (SELECT Rate from donorgroup where donorgroup.ID = MP.DonorGroup_ID)) , 2 ) as 'القرض ($)'
,L.Loan_Rate as 'النسبة' 
,L.Loan_ReturnedAmount as 'المبلغ المطلوب استرداده'
,L.Loan_PaymentsAmount as 'القسط'
,L.Loan_PaymentsCount as 'عدد الدفعات الكلي'
,(select count(Pay_Amount) from `payment` where `Loan_ID` = L.Loan_ID and `Pay_IsPaid` like N'Paid') as 'عدد الدفعات المدفوعة'
,((IF(((L.Loan_Amount * L.Loan_Rate / 100) - (select sum(`Pay_Amount`) from `payment` where `Pay_IsPaid` like N'Paid' and `Loan_ID` = L.Loan_ID))<0 
    ,0,((L.Loan_Amount * L.Loan_Rate / 100) - (select sum(`Pay_Amount`) from `payment` where `Pay_IsPaid` like N'Paid' and `Loan_ID` = L.Loan_ID))) )
    / L.Loan_PaymentsAmount ) as 'عدد الدفعات المتبقية'
,(select sum(`Pay_Amount`) from `payment` where `Pay_IsPaid` like N'Paid' and `Loan_ID` = L.Loan_ID) as 'المبلغ المدفوع'
,IF(((L.Loan_Amount * L.Loan_Rate / 100) - (select sum(`Pay_Amount`) from `payment` where `Pay_IsPaid` like N'Paid' and `Loan_ID` = L.Loan_ID))<0
    ,0,((L.Loan_Amount * L.Loan_Rate / 100) - (select sum(`Pay_Amount`) from `payment` where `Pay_IsPaid` like N'Paid' and `Loan_ID` = L.Loan_ID))) as 'المبلغ المتبقي'

,(SELECT `Pay_DueDate` FROM `payment` WHERE `Loan_ID` = L.Loan_ID and `Pay_IsPaid` like 'Paid' ORDER BY `payment`.`Pay_DueDate` DESC LIMIT 1) as 'تاريخ آخر دفعة مدفوعة'
,(SELECT `Pay_DueDate` FROM `payment` WHERE `Loan_ID` = L.Loan_ID and `Pay_IsPaid` like 'Not Paid'  AND state.Name_ar like 'ممول' ORDER BY `payment`.`Pay_DueDate` DESC LIMIT 1) as 'تاريخ الدفعة المستحقة'
,(SELECT TIMESTAMPDIFF(MONTH, payment.Pay_DueDate , DATE_ADD( Now(),INTERVAL 1 MONTH)) from payment where payment.Loan_ID = L.Loan_ID and payment.Pay_IsPaid = 'Not Paid' AND state.Name_ar like 'ممول' ORDER by Pay_DueDate desc LIMIT 1 ) as 'عدد أشهر التأخير'

,donorgroup.Name as 'المجموعة'
,L.Loan_ReceiptID as 'رقم الإيصال'

from `loan` L 
 left outer join `microproject` MP on L.MicroProject_ID = MP.MP_ID 
 LEFT outer join person_microproject PMP on PMP.MicroProject_ID = MP.MP_ID 
 LEFT outer join person P on P.P_ID = PMP.Person_ID 
 LEFT outer join `category` C on MP.MP_Category_ID = C.C_ID 
 LEFT outer join `state` on state.ID = MP.MP_State 
 LEFT outer join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID;
 
 END//