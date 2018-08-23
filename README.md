# HANA Work
Things I've worked on and learned about at Hana during my summer internship there.
Main project is to create a survey template (along the lines of google forms) so that staff in HR can create surveys about employee satisfaction in various areas and then get other members of staff to fill it out and view statistics. I am working on this project with one of the senior systems analyst.

TO DO:
- Add a section delete button on adminCreate -> add some sort of flag to each question so we know which section it is part of, then we can delete all the questions within the section.
- Fix the issue on adminCreate where if you add many questions in a row without choosing answer types, when you click on the last question to add an answer, the answer type will be assigned to the top available question.
- IN PROGRESS: Make a page to show graphs and summaries of an individual survey
- IN PROGRESS: close dates
		-> Have a manual button so the admin can close the survey before the chosen close date if desired.
		-> manual button doesn't fill box atm.
- Have an option so the admin can choose who they want to send the email to -> mail button will go to another page where there is a list of all employees and their emails. admin can select users from that list to email about that specific survey.
- IN PROGRESS: testing
- Get users logged in by their passwords so can differentiate between admins and users.

GETTING DATA FOR STATISTICS

/*This gets the number of users who have answered the question with Id 25 from the survey with ID 3*/
declare @xsubjectId Integer = 3
declare @xquestionId Integer = 25
select count(*) as cntSubmit
	from surveySection a
	inner join surveyMaster b on a.subjectId = b.subjectId
	inner join surveyQuestion c on a.sectionId = c.sectionId
	inner join surveyUserAnswer d on c.questionId = d.questionId
	where b.subjectId = @xsubjectId and c.questionId = @xquestionId
	
/*This gets the number of users who have answered option 89 from question 25 from survey 3*/
declare @xsubjectId Integer = 3
declare @xquestionId Integer = 25
declare @xanswerId Integer = 89
select b.subjectName, a.sectionId, a.sectionName, c.questionId, c.questionName, c.questionType, d.answerId
	from surveySection a
	inner join surveyMaster b on a.subjectId = b.subjectId
	inner join surveyQuestion c on a.sectionId = c.sectionId
	inner join surveyUserAnswer d on c.questionId = d.questionId
	inner join surveyAnswer e on e.answerId = d.answerId
	where b.subjectId = @xsubjectId and c.questionId = @xquestionId and d.answerId = @xanswerId
	
/*This gets the number of users who have answered each option from question 25 from survey 3*/
declare @xsubjectId Integer = 3
declare @xquestionId Integer = 27

select b.subjectName, a.sectionId, a.sectionName, c.questionId, c.questionName, c.questionType, d.answerId
,count(*)  as cnt
	from surveySection a
	inner join surveyMaster b on a.subjectId = b.subjectId
	inner join surveyQuestion c on a.sectionId = c.sectionId
	inner join surveyUserAnswer d on c.questionId = d.questionId
	where b.subjectId = @xsubjectId and c.questionId = @xquestionId
	group by b.subjectName, a.sectionId, a.sectionName, c.questionId, c.questionName, c.questionType, d.answerId
	order by answerId
	