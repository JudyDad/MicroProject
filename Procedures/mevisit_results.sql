DROP PROCEDURE IF EXISTS mevisit_results;
DELIMITER //
create procedure mevisit_results(IN MyCondition text,IN MyType text)
BEGIN

SET @inner_condition1 = ' ';
IF (MyCondition <> '') THEN 
SET @inner_condition1 = concat(@inner_condition1,' ',MyCondition); End IF; 
SET @where1 = ' where 1 ';

SET @sql1 = concat(',(SELECT count(*) from `mevisit_answer` 
			left join `mevisit` on mevisit.ID = mevisit_answer.Visit_ID 
			where mevisit_answer.Question_ID = mequestion.ID and mevisit_answer.Answer_ID = ans1_ID '
			,@inner_condition1
			,') as ''ans1_count'' ');
SET @sql2 = concat(',(SELECT count(*) from `mevisit_answer` 
			left join `mevisit` on mevisit.ID = mevisit_answer.Visit_ID 
			where mevisit_answer.Question_ID = mequestion.ID and mevisit_answer.Answer_ID = ans2_ID '
			,@inner_condition1
			,') as ''ans2_count'' ');
SET @sql3 = concat(',(SELECT count(*) from `mevisit_answer` 
			left join `mevisit` on mevisit.ID = mevisit_answer.Visit_ID 
			where mevisit_answer.Question_ID = mequestion.ID and mevisit_answer.Answer_ID = ans3_ID '
			,@inner_condition1
			,') as ''ans3_count'' ');

SET @sql4 = concat(',(SELECT count(*) from `mevisit_answer` 
			left join `mevisit` on mevisit.ID = mevisit_answer.Visit_ID 
			where mevisit_answer.Question_ID = mequestion.ID '
			,@inner_condition1
			,') as ''all_ans_count'' ');			
                
SET @sql_all = concat('SELECT mequestion.ID
, Name
, (SELECT Name from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID ASC LIMIT 1) as ''ans1''

, IFNULL( (SELECT Name from meanswer WHERE meanswer.Question_ID = mequestion.ID 
   and meanswer.ID >  (SELECT ID from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID ASC LIMIT 1) 
   and meanswer.ID <  (SELECT ID from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID DESC LIMIT 1)  
   LIMIT 1) ,'''') as ''ans2''

, (SELECT Name from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID DESC LIMIT 1) as ''ans3''

, (SELECT ID from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID ASC LIMIT 1) as ''ans1_ID''

, IFNULL( (SELECT ID from meanswer WHERE meanswer.Question_ID = mequestion.ID
   and meanswer.ID > (SELECT ID from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID ASC LIMIT 1)
   and meanswer.ID < (SELECT ID from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID DESC LIMIT 1)  
   LIMIT 1) ,'''' ) as ''ans2_ID''

, (SELECT ID from meanswer WHERE meanswer.Question_ID = mequestion.ID ORDER by ID DESC LIMIT 1) as ''ans3_ID'''

,@sql1
,@sql2
,@sql3
,@sql4
,' FROM `mequestion` ');

IF (MyType <> '') THEN 
SET @where1 = concat(@where1,' and mequestion.`Type` like ''%" + type + "%'' ',MyType); End IF; 
SET @orderBy1 = ' order by ID ';

set @query = concat(@sql_all,@where1,@orderBy1);

PREPARE stmt FROM @query;
EXECUTE stmt;  

END//