var questionsIndex = 1;

$(document).ready(function () {
	showOrHideRemoveQuestion();
	
	$(".datepicker").datepicker({
		dateFormat: 'dd/mm/yy',
		firstDay: 1,
		goToCurrent: true,
		minDate: new Date()
	});
	$("#startDate").datepicker("setDate", new Date());
	$("#startDate").change(function() {
		$("#endDate").datepicker("option", "minDate", $("#startDate").val());
		enableOrDisableSubmitButton();
	});
	$("#endDate").change(function() {
		$("#startDate").datepicker("option", "maxDate", $("#endDate").val());
		enableOrDisableSubmitButton();
	});
	
	$("#name").keyup(function() {
		enableOrDisableSubmitButton();
	});
});

function enableOrDisableSubmitButton()
{
	if($("#name").val() == "" || $("#startDate").val() == "" || $("#endDate").val() == "") {
		$("#submitEvent").prop("disabled", true);
	}
	else $("#submitEvent").prop("disabled", false);
}

function allowDrop(ev)
{
	ev.preventDefault();
}

function drag(ev)
{
	ev.dataTransfer.setData("Text",ev.target.id);
}

function drop(ev)
{
	ev.preventDefault();
	var data = ev.dataTransfer.getData("Text");
	ev.target.appendChild(document.getElementById(data));
}

function showOrHideRemoveQuestion()
{
	if($("#questionsDiv > div").length > 1) {
		$(".removeQuestion").show();
	}
	else {
		$(".removeQuestion").hide();
	}
}

function addQuestion()
{
	var questionDiv = document.createElement("div");
	questionDiv.setAttribute("id", ("questionDiv").concat(questionsIndex));
	
	var fieldset = document.createElement("fieldset");
	
	var removeQuestion = document.createElement("img");
	removeQuestion.setAttribute("src", "../resources/images/close.png");
	removeQuestion.setAttribute("alt", "Remove this question");
	removeQuestion.setAttribute("width", "25px");
	removeQuestion.setAttribute("height", "25px");
	removeQuestion.setAttribute("id", ("removeQuestion").concat(questionsIndex));
	removeQuestion.setAttribute("class", "removeQuestion");
	removeQuestion.setAttribute("onclick", ("removeQuestion(").concat(questionsIndex).concat(")"));
	
	var questionInput = document.createElement("input");
	questionInput.setAttribute("type", "text");
	questionInput.setAttribute("id", ("questions[").concat(questionsIndex).concat("].question"));
	questionInput.setAttribute("name", ("questions[").concat(questionsIndex).concat("].question"));
	questionInput.setAttribute("placeholder", "Enter the question");
	
	var selectQuestionType = document.createElement("select");
	selectQuestionType.setAttribute("id", ("questions[").concat(questionsIndex).concat("].questionType"));
	selectQuestionType.setAttribute("name", ("questions[").concat(questionsIndex).concat("].questionType"));
	
	var multipleChoice = document.createElement("option");
	multipleChoice.setAttribute("value", "1");
	multipleChoice.innerHTML = "Multiple choice";
	var checkboxes = document.createElement("option");
	checkboxes.setAttribute("value", "2");
	checkboxes.innerHTML = "Checkboxes";
	var textnumber = document.createElement("option");
	textnumber.setAttribute("value", "3");
	textnumber.innerHTML = "Text/number";
	
	selectQuestionType.appendChild(multipleChoice);
	selectQuestionType.appendChild(checkboxes);
	selectQuestionType.appendChild(textnumber);
	
	var br = document.createElement("br");
	var br2 = document.createElement("br");
	
	var div = document.createElement("div");
	div.setAttribute("id", ("answersDiv").concat(questionsIndex));
	
	var answer1Input = document.createElement("input");
	answer1Input.setAttribute("type", "text");
	answer1Input.setAttribute("id", ("questions[").concat(questionsIndex).concat("].answers[0].answer"));
	answer1Input.setAttribute("name", ("questions[").concat(questionsIndex).concat("].answers[0].answer"));
	answer1Input.setAttribute("placeholder", "Enter an answer-option");
	
	var answer2Input = document.createElement("input");
	answer2Input.setAttribute("type", "text");
	answer2Input.setAttribute("id", ("questions[").concat(questionsIndex).concat("].answers[1].answer"));
	answer2Input.setAttribute("name", ("questions[").concat(questionsIndex).concat("].answers[1].answer"));
	answer2Input.setAttribute("placeholder", "Enter an answer-option");
	
	div.appendChild(answer1Input);
	div.appendChild(answer2Input);
	
	var addAnswer = document.createElement("input");
	addAnswer.setAttribute("type", "button");
	addAnswer.setAttribute("value", "Add answer");
	addAnswer.setAttribute("id", ("addAnswer").concat(questionsIndex));
	addAnswer.setAttribute("class", "button");
	addAnswer.setAttribute("onclick", ("addAnswer(").concat(questionsIndex).concat(", 2)"));
	
	fieldset.appendChild(removeQuestion);
	fieldset.appendChild(questionInput);
	fieldset.appendChild(selectQuestionType);
	fieldset.appendChild(br);
	fieldset.appendChild(br2);
	fieldset.appendChild(div);
	fieldset.appendChild(addAnswer);
	
	questionDiv.appendChild(fieldset);
	
	document.getElementById("questionsDiv").appendChild(questionDiv);
	
	questionsIndex = questionsIndex + 1;
	showOrHideRemoveQuestion();
}

function addAnswer(question, answerIndex)
{
	var answerInput = document.createElement("input");
	answerInput.setAttribute("type", "text");
	answerInput.setAttribute("id", (("questions[").concat(question).concat("].answers[").concat(answerIndex).concat("].answer")));
	answerInput.setAttribute("name", (("questions[").concat(question).concat("].answers[").concat(answerIndex).concat("].answer")));
	answerInput.setAttribute("placeholder", "Enter an answer-option");
	
	document.getElementById(("answersDiv").concat(question)).appendChild(answerInput);
	document.getElementById(("addAnswer").concat(question)).onclick = function () {addAnswer(question, (answerIndex+1));};
}

function removeQuestion(question)
{
	var questionToRemove = document.getElementById(("questionDiv").concat(question));
	questionsDiv.removeChild(questionToRemove);
	showOrHideRemoveQuestion();
}

function removeAnswer(question, answer)
{
	
}