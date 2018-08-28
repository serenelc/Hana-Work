# HANA Work
Things I've worked on and learned about at Hana during my summer internship there.
Main project is to create a survey template (along the lines of google forms) so that staff in HR can create surveys about employee satisfaction in various areas and then get other members of staff to fill it out and view statistics. I am working on this project with one of the senior systems analyst.
Using vb.net, sql, html, css, asp.net and javascript.

TO DO:
- IN PROGRESS: Add a section delete button on adminCreate -> add some sort of flag to each question so we know which section it is part of, then we can delete all the questions within the section.
- Fix the issue on adminCreate where if you add many questions in a row without choosing answer types, when you click on the last question to add an answer, the answer type will be assigned to the top available question.
- IN PROGRESS: Graphs and summaries of data 
	-> table with percentages
	-> Section titles more obvious. Maybe an underline after the end of each section? Only 1 section title per section not 1 title per graph.
	-> For the short answer grid, if manage to make a section title obvious then just put answerComment in table with table title = questionName. Can ignore sectionName.
- Email confirmation pop up line 105 doesn't seem to appear
- dialog boxes don't work once the website is uploaded to the server.