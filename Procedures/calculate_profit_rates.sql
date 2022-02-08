Drop procedure IF EXISTS calculate_profit_rates;

DELIMITER $$
CREATE PROCEDURE `calculate_profit_rates`(IN `MyCondition` TEXT)
BEGIN 
  
DROP TABLE IF EXISTS t1;
CREATE TABLE t1
 
 SELECT mevisit.Person_ID as 'Person_ID'
 
 ,(SELECT IFNULL(avg(W_Salary),0) FROM `person_workexp` WHERE `W_Status` like 'Yes' and `Person_ID` =  mevisit.Person_ID) as 'AvgSal'
 
 ,(SELECT IFNULL(avg(me.`Profit`),0) FROM `mevisit` me WHERE me.`Person_ID` = mevisit.Person_ID) as 'AvgEval' 
 
 , (case WHEN   
	(
		(SELECT IFNULL(avg(W_Salary),0) FROM `person_workexp` WHERE `W_Status` like 'Yes' and `Person_ID` =  mevisit.Person_ID) = 0 
		AND 
		(SELECT IFNULL(avg(me.`Profit`),0) FROM `mevisit` me WHERE me.`Person_ID` = mevisit.Person_ID) = 0 
	)
	THEN '-1000000000' 
	WHEN   
	(
		(SELECT IFNULL(avg(W_Salary),0) FROM `person_workexp` WHERE `W_Status` like 'Yes' and `Person_ID` =  mevisit.Person_ID) = 0 
		AND 
		(SELECT IFNULL(avg(me.`Profit`),0) FROM `mevisit` me WHERE me.`Person_ID` = mevisit.Person_ID) <> 0
	)
	THEN '100' 
	ELSE 
		(	
			(
			 (SELECT IFNULL(avg(me.`Profit`),0) FROM `mevisit` me WHERE me.`Person_ID` = mevisit.Person_ID)
			 - 
			 (SELECT IFNULL(avg(W_Salary),0) FROM `person_workexp` WHERE `W_Status` like 'Yes' and `Person_ID` =  mevisit.Person_ID)
			)
			/
			(SELECT IFNULL(avg(W_Salary),0) FROM `person_workexp` WHERE `W_Status` like 'Yes' and `Person_ID` =  mevisit.Person_ID)
		)*100 
	End )as 'EvalSal_Percentage'
                    
	FROM mevisit
	left join `person_microproject` PMP  on mevisit.Person_ID = PMP.Person_ID
	left join `microproject` MP  on PMP.MicroProject_ID = MP.MP_ID
	left join `person` P on PMP.Person_ID = P.P_ID
	left join `loan` L on MP.MP_ID = L.MicroProject_ID 
	left join `category` C on MP.MP_Category_ID = C.C_ID
	LEFT JOIN `subcategory` sub on sub.ID =  MP.SubCategory_ID 
	LEFT join `state` on MP.MP_State = state.ID 
	Left join `donor` on MP.MP_Donor = donor.ID
	LEFT JOIN `donorgroup` on donorgroup.ID = MP.DonorGroup_ID  
	LEFT JOIN `fundtype` on fundtype.ID = MP.MP_FundType_ID 
	LEFT JOIN `microprojecttype` on microprojecttype.ID = MP.MP_Type_ID
	LEFT JOIN `microprojectsubtype` on microprojectsubtype.ID = MP.MP_SubType_ID
                    
 WHERE PMP.PersonType like 'مستفيد'					
 GROUP BY mevisit.Person_ID;
 
SET @sql1 = 'SELECT CASE 
 when t1.EvalSal_Percentage BETWEEN -999999999 and -1 then ''<0 %''
 when t1.EvalSal_Percentage = 0 then ''0 %''
 when t1.EvalSal_Percentage BETWEEN 1 and 20 then ''1-20 %''
 when t1.EvalSal_Percentage BETWEEN 20 and 40 then ''21-40 %'' 
 when t1.EvalSal_Percentage BETWEEN 40 and 60 then ''41-60 %''
 when t1.EvalSal_Percentage BETWEEN 60 and 80 then ''61-80 %'' 
 when t1.EvalSal_Percentage BETWEEN 80 and 99 then ''81-99 %'' 
 when t1.EvalSal_Percentage = 100 then ''100 %'' 
 when t1.EvalSal_Percentage > 100 then ''>100 %'' 
 when t1.EvalSal_Percentage = -1000000000 then ''N/A'' 
 
 else ''bubu'' End as ''Rate'' 
 
 ,count(DISTINCT t1.Person_ID) as ''numOfProjects''

 FROM t1 
 left join `person_microproject` PMP  on PMP.Person_ID = t1.Person_ID
 left join `person` P on P.P_ID = PMP.Person_ID 
 left join `microproject` MP  on PMP.MicroProject_ID = MP.MP_ID  
 left join `loan` L on L.MicroProject_ID = MP.MP_ID 
 left join `category` C on MP.MP_Category_ID = C.C_ID 
 Left join `state` on state.ID = MP.MP_State 
 Left join `donor` on donor.ID = MP.MP_Donor';

SET @where1 = ' ';
SET @groupBy1 = ' group by Rate ORDER BY t1.`EvalSal_Percentage` ';
  
IF (MyCondition <> '') THEN 
SET @where1 = concat(@where1,' ',MyCondition); End IF; 
 
SET @sql1 = concat(@sql1,' ',@where1,' ',@groupBy1);

PREPARE stmt FROM @sql1;
EXECUTE stmt;  
DROP TABLE IF EXISTS t1;
END$$
DELIMITER ;