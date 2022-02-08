Drop PROCEDURE if EXISTS empty_database;
DELIMITER $$
CREATE PROCEDURE `empty_database`()
BEGIN
DELETE from checklist_member;
DELETE FROM donorgroup;
DELETE from exefile;
DELETE from family;
DELETE from file;
DELETE from image;
DELETE from loan;
DELETE from log;
DELETE from material;
DELETE from mevisit;
DELETE from mevisit_answer;
DELETE from mevisit_user;
DELETE from microproject;
DELETE from microproject_score;
DELETE from microproject_risk;
DELETE from office_monitoring;
DELETE from payment;
DELETE from person;
DELETE from person_course;
DELETE from person_education;
DELETE from person_family;
DELETE from person_loss;
DELETE from person_microproject;
DELETE from person_workexp;
DELETE from priest;
DELETE from task_microproject;
DELETE from task_user;
DELETE from user_notification;
DELETE from visit;
DELETE from visit_user; 
END$$
DELIMITER ;