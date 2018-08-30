# HANA Work
Things I've worked on and learned about at Hana during my summer internship there.
Main project is to create a survey template (along the lines of google forms) so that staff in HR can create surveys about employee satisfaction in various areas and then get other members of staff to fill it out and view statistics. I am working on this project with one of the senior systems analyst.
Using vb.net, sql, html, css, asp.net and javascript.

TO DO:
- IMPORTANT: Add a section delete button on adminCreate -> add some sort of flag to each question so we know which section it is part of, then we can delete all the questions within the section.
- Fix the issue on adminCreate where if you add many questions in a row without choosing answer types, when you click on the last question to add an answer, the answer type will be assigned to the top available question.
- Graphs and summaries of data 
	-> table with percentages
	-> Only 1 section title per section not 1 title per graph. (not v important)
- Email confirmation pop up line 105 doesn't seem to appear

After meeting with user, TO DO:
- Redo process flow and manual
- Logout button won't float right on userSurveyList page
- Make the new login page look nicer
- UserSurveyComplete need to check for Session("EN") to show logout button if exists and not if it's just a Go one. Also make sure the session clears if it's a login one when it goes back to userSurveyList
	-> If a user is an admin the userType = ADMIN
	-> If a user logged in to answer a survey the userType = USER
	-> If a user didn't log in to answer a survey, the userType = ""