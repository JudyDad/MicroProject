DROP PROCEDURE IF EXISTS `visit_user_statistics`;
DELIMITER $$
CREATE PROCEDURE `visit_user_statistics`(IN `FromDate` TINYTEXT, IN `ToDate` TINYTEXT)
BEGIN
SELECT UserName as 'الزائر',
	(SELECT COUNT(v.ID) FROM visit v LEFT JOIN visit_user vu ON vu.Visit_ID = v.ID  
 WHERE v.Kind like '%op' and vu.User_ID = visit_user.User_ID
 AND v.Date between FromDate AND ToDate ) as 'Opening',
	(SELECT COUNT(me.ID) FROM mevisit me  
 LEFT JOIN mevisit_user meu ON meu.Visit_ID = me.ID  WHERE me.Number='1' and meu.User_ID = mevisit_user.User_ID
 AND me.Date between FromDate AND ToDate ) as 'M1',
     (SELECT COUNT(me.ID) FROM mevisit me  
 LEFT JOIN mevisit_user meu ON meu.Visit_ID = me.ID  WHERE me.Number='2' and meu.User_ID = mevisit_user.User_ID AND me.Date between FromDate AND ToDate) as 'M2',
     (SELECT COUNT(me.ID) FROM mevisit me  
 LEFT JOIN mevisit_user meu ON meu.Visit_ID = me.ID  WHERE me.Number='3' and meu.User_ID = mevisit_user.User_ID AND me.Date between FromDate AND ToDate) as 'M3',
     (SELECT COUNT(me.ID) FROM mevisit me  
 LEFT JOIN mevisit_user meu ON meu.Visit_ID = me.ID  WHERE me.Number='4' and meu.User_ID = mevisit_user.User_ID AND me.Date between FromDate AND ToDate) as 'M4',
     (SELECT COUNT(me.ID) FROM mevisit me  
 LEFT JOIN mevisit_user meu ON meu.Visit_ID = me.ID  WHERE me.Number='5' and meu.User_ID = mevisit_user.User_ID AND me.Date between FromDate AND ToDate) as 'M5',
	(SELECT COUNT(v.ID) FROM visit v LEFT JOIN visit_user vu ON vu.Visit_ID = v.ID
 WHERE v.Kind like '%cl' and vu.User_ID = visit_user.User_ID
 AND v.Date between FromDate AND ToDate ) as 'Closing',
   
   (SELECT COUNT(v.ID) FROM visit v LEFT JOIN visit_user vu ON vu.Visit_ID = v.ID  
 WHERE  vu.User_ID = visit_user.User_ID AND v.Date between FromDate AND ToDate) 
 +  
     (SELECT COUNT(me.ID) FROM mevisit me LEFT JOIN mevisit_user meu ON meu.Visit_ID = me.ID  WHERE meu.User_ID = mevisit_user.User_ID AND me.Date between FromDate AND ToDate) 
 as 'مجموع الزيارات'
  
 From `user`  
 LEFT JOIN `visit_user` ON visit_user.User_ID = user.UserID 
 LEFT JOIN `visit` ON visit_user.Visit_ID = visit.ID
 LEFT JOIN `mevisit_user` ON mevisit_user.User_ID = user.UserID  
 LEFT join `mevisit` on mevisit_user.Visit_ID = mevisit.ID
 
 WHERE user.IsVisitor = 1 
 AND (visit.Date between FromDate AND ToDate
 OR mevisit.Date between FromDate AND ToDate)
 GROUP BY `الزائر`  
 ORDER BY `مجموع الزيارات`  DESC;
 
-- SELECT IFNULL(COUNT(*),0) as 'sum op' from visit where `Kind` like '%op' and `Date` between FromDate AND ToDate;
-- SELECT IFNULL(COUNT(*),0) as 'sum cl' from visit where `Kind` like '%cl' and `Date` between FromDate AND ToDate;
-- SELECT IFNULL(COUNT(*),0) as 'sum m1' FROM mevisit where `Number`='1' and `Date` between FromDate AND ToDate;
-- SELECT IFNULL(COUNT(*),0) as 'sum m2' FROM mevisit where `Number`='2' and `Date` between FromDate AND ToDate;
-- SELECT IFNULL(COUNT(*),0) as 'sum m3' FROM mevisit where `Number`='3' and `Date` between FromDate AND ToDate;
-- SELECT IFNULL(COUNT(*),0) as 'sum m4' FROM mevisit where `Number`='4' and `Date` between FromDate AND ToDate;
-- SELECT IFNULL(COUNT(*),0) as 'sum m5' FROM mevisit where `Number`='5' and `Date` between FromDate AND ToDate;
-- SELECT IFNULL(COUNT(visit.ID),0) 
    -- + (SELECT IFNULL(COUNT(mevisit.ID),0) FROM mevisit where `Date` between FromDate AND ToDate) as 'total'
	-- FROM visit where `Date` between FromDate AND ToDate;
	
SELECT  
      IFNULL(COUNT(case when visit.`Kind` like '%op' then 1 end ),0) as 'sum op' 
    , IFNULL(COUNT(case when visit.`Kind` like '%cl' then 1 end),0) as 'sum cl' 
    , IFNULL(COUNT(visit.ID),0) as 'total visit'  
    from `visit`
	LEFT join `microproject` MP on visit.MicroProject_ID = MP.MP_ID
    LEFT join `category` C on MP.MP_Category_ID = C.C_ID 
    LEFT join `state` on state.ID = MP.MP_State  
    LEFT join `donor` on donor.ID = MP.MP_Donor 
    LEFT join `loan` L on L.MicroProject_ID = MP.MP_ID
    LEFT join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID 
where visit.`Date` between FromDate AND ToDate ;

SELECT
      IFNULL(COUNT(CASE WHEN `Number`= '1' THEN 1 end), 0) as 'sum m1'
    , IFNULL(COUNT(CASE WHEN `Number`= '2' THEN 1 end), 0) as 'sum m2'
    , IFNULL(COUNT(CASE WHEN `Number`= '3' THEN 1 end), 0) as 'sum m3'
    , IFNULL(COUNT(CASE WHEN `Number`= '4' THEN 1 end), 0) as 'sum m4'
    , IFNULL(COUNT(CASE WHEN `Number`= '5' THEN 1 end), 0) as 'sum m5'
    , IFNULL(COUNT(mevisit.ID), 0) as 'total mevisit'  
from `microproject` MP
LEFT join `person_microproject` PMP on MP.MP_ID = PMP.MicroProject_ID 
LEFT join `mevisit` on PMP.Person_ID = mevisit.Person_ID 
LEFT join `category` C on MP.MP_Category_ID = C.C_ID 
LEFT join `state` on state.ID = MP.MP_State  
LEFT join `donor` on donor.ID = MP.MP_Donor  
LEFT join `loan` L on L.MicroProject_ID = MP.MP_ID
LEFT join `donorgroup` on donorgroup.ID = MP.DonorGroup_ID 
where mevisit.`Date` between FromDate AND ToDate and PMP.PersonType like 'مستفيد' ;




                
END$$
DELIMITER ;