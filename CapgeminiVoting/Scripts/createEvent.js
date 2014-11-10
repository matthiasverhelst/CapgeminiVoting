$(document).ready(function () {
    showOrHideRemoveQuestion();
    showOrHideRemoveAnswer(0);

	$(".datepicker").datepicker({
		dateFormat: 'dd/mm/yy',
		firstDay: 1,
		goToCurrent: true,
		minDate: new Date()
	});
	$("#StartDate").datepicker("setDate", new Date());
	$("#StartDate").change(function() {
		$("#EndDate").datepicker("option", "minDate", $("#StartDate").val());
		enableOrDisableSubmitButton();
	});
	$("#EndDate").change(function() {
		$("#StartDate").datepicker("option", "maxDate", $("#EndDate").val());
		enableOrDisableSubmitButton();
	});
	
	$("#Name").keyup(function() {
		enableOrDisableSubmitButton();
	});
});

function bindQuestionTypeTrigger()
{
    $(".questionType").change(function () {
        if ($(this).val() == "3") {
            $(this).parents().eq(1).find(".answersDiv").hide();
            $(this).parents().eq(1).find(".addAnswerDiv").hide();
        }
        else {
            $(this).parents().eq(1).find(".answersDiv").show();
            $(this).parents().eq(1).find(".addAnswerDiv").show();
        }
    });
}

function enableOrDisableSubmitButton()
{
	if($("#Name").val() == "" || $("#StartDate").val() == "" || $("#EndDate").val() == "") {
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

function showOrHideRemoveAnswer(question)
{
    var answersDiv = ("#answersDiv").concat(question);
    var removeAnswerClass = (".removeAnswer").concat(question);
    if ($(answersDiv.concat(" > input")).length > 1) {
        $(removeAnswerClass).show();
    }
    else {
        $(removeAnswerClass).hide();
    }
}

function addQuestion(questionsIndex)
{
	var questionDiv = document.createElement("div");
	questionDiv.setAttribute("id", ("questionDiv").concat(questionsIndex));
	questionDiv.setAttribute("class", "eventDetails");
	
	var removeQuestion = document.createElement("img");
	removeQuestion.setAttribute("src", "/Images/close.png");
	removeQuestion.setAttribute("alt", "Remove this question");
	removeQuestion.setAttribute("id", ("removeQuestion").concat(questionsIndex));
	removeQuestion.setAttribute("class", "removeQuestion");
	removeQuestion.setAttribute("onclick", ("removeQuestion(").concat(questionsIndex).concat(")"));
	
	var question = document.createElement("div");
	question.setAttribute("class", "form-group");

	var questionLabel = document.createElement("label");
	questionLabel.setAttribute("for", "Question");
	questionLabel.innerHTML = "Question";

	var questionInput = document.createElement("input");
	questionInput.setAttribute("type", "text");
	questionInput.setAttribute("id", ("questions[").concat(questionsIndex).concat("].question"));
	questionInput.setAttribute("name", ("questions[").concat(questionsIndex).concat("].question"));
	questionInput.setAttribute("placeholder", "Enter your question");
	questionInput.setAttribute("class", "form-control");

	question.appendChild(questionLabel);
	question.appendChild(questionInput);
	
	var questionType = document.createElement("div");
	questionType.setAttribute("class", "form-group");

	var questionTypeLabel = document.createElement("label");
	questionTypeLabel.setAttribute("for", "Type");
	questionTypeLabel.innerHTML = "Type";

	var selectQuestionType = document.createElement("select");
	selectQuestionType.setAttribute("id", ("questions[").concat(questionsIndex).concat("].questionType"));
	selectQuestionType.setAttribute("name", ("questions[").concat(questionsIndex).concat("].questionType"));
	selectQuestionType.setAttribute("class", "form-control questionType");

	var multipleChoice = document.createElement("option");
	multipleChoice.setAttribute("value", "1");
	multipleChoice.innerHTML = "Multiple choice";
	var checkboxes = document.createElement("option");
	checkboxes.setAttribute("value", "2");
	checkboxes.innerHTML = "Checkboxes";
	var textnumber = document.createElement("option");
	textnumber.setAttribute("value", "3");
	textnumber.innerHTML = "Free text";
	
	selectQuestionType.appendChild(multipleChoice);
	selectQuestionType.appendChild(checkboxes);
	selectQuestionType.appendChild(textnumber);
	
	questionType.appendChild(questionTypeLabel);
	questionType.appendChild(selectQuestionType);

	var div = document.createElement("div");
	div.setAttribute("id", ("answersDiv").concat(questionsIndex));
	div.setAttribute("class", "form-group answersDiv");

	var answerLabel = document.createElement("label");
	answerLabel.setAttribute("for", "Possible_answers");
	answerLabel.innerHTML = "Possible answers";

	var answerInput = document.createElement("input");
	answerInput.setAttribute("type", "text");
	answerInput.setAttribute("id", ("questions[").concat(questionsIndex).concat("].answers[0].answer"));
	answerInput.setAttribute("name", ("questions[").concat(questionsIndex).concat("].answers[0].answer"));
	answerInput.setAttribute("placeholder", "Enter your answer");
	answerInput.setAttribute("class", "form-control answer");
	
	var removeAnswer = document.createElement("img");
	removeAnswer.setAttribute("src", "../Images/close.png");
	removeAnswer.setAttribute("alt", "Remove this answer");
	removeAnswer.setAttribute("id", ("removeAnswer").concat(questionsIndex).concat("0"));
	removeAnswer.setAttribute("class", ("removeAnswer removeAnswer").concat(questionsIndex));
	removeAnswer.setAttribute("onclick", ("removeAnswer(").concat(questionsIndex).concat(",0)"));

	div.appendChild(answerLabel);
	div.appendChild(answerInput);
	div.appendChild(removeAnswer);
	
	var addAnswerDiv = document.createElement("div");
	addAnswerDiv.setAttribute("class", "form-group addAnswerDiv");

	var addAnswer = document.createElement("input");
	addAnswer.setAttribute("type", "button");
	addAnswer.setAttribute("value", "Add answer");
	addAnswer.setAttribute("id", ("addAnswer").concat(questionsIndex));
	addAnswer.setAttribute("class", "btn btn-primary");
	addAnswer.setAttribute("onclick", ("addAnswer(").concat(questionsIndex).concat(", 1)"));
	
	addAnswerDiv.appendChild(addAnswer);

	questionDiv.appendChild(removeQuestion);
	questionDiv.appendChild(question);
	questionDiv.appendChild(questionType);
	questionDiv.appendChild(div);
	questionDiv.appendChild(addAnswerDiv);
	
	document.getElementById("questionsDiv").appendChild(questionDiv);
	
	document.getElementById("addQuestionBtn").onclick = function () { addQuestion(questionsIndex + 1); };

	showOrHideRemoveQuestion();
	showOrHideRemoveAnswer(questionsIndex);
	bindQuestionTypeTrigger();
}

function addAnswer(question, answer)
{
	var answerInput = document.createElement("input");
	answerInput.setAttribute("type", "text");
	answerInput.setAttribute("id", (("questions[").concat(question).concat("].answers[").concat(answer).concat("].answer")));
	answerInput.setAttribute("name", (("questions[").concat(question).concat("].answers[").concat(answer).concat("].answer")));
	answerInput.setAttribute("placeholder", "Enter your answer");
	answerInput.setAttribute("class", "form-control answer");

	var removeAnswer = document.createElement("img");
	removeAnswer.setAttribute("src", "../Images/close.png");
	removeAnswer.setAttribute("alt", "Remove this answer");
	removeAnswer.setAttribute("id", ("removeAnswer").concat(question).concat(answer));
	removeAnswer.setAttribute("class", ("removeAnswer removeAnswer").concat(question));
	removeAnswer.setAttribute("onclick", ("removeAnswer(").concat(question).concat(",").concat(answer).concat(")"));
	
	document.getElementById(("answersDiv").concat(question)).appendChild(answerInput);
	document.getElementById(("answersDiv").concat(question)).appendChild(removeAnswer);
	document.getElementById(("addAnswer").concat(question)).onclick = function () { addAnswer(question, (answer + 1)); };

	showOrHideRemoveAnswer(question);
}

function removeQuestion(question)
{
    var questionToRemove = document.getElementById(("questionDiv").concat(question));
    var questionsDiv = document.getElementById("questionsDiv")
    questionsDiv.removeChild(questionToRemove);

	showOrHideRemoveQuestion();
}

function removeAnswer(question, answer)
{
    var answerToRemove = document.getElementById(("questions[").concat(question).concat("].answers[").concat(answer).concat("].answer"));
    var closeImageToRemove = document.getElementById(("removeAnswer").concat(question).concat(answer));
    var answersDiv = document.getElementById(("answersDiv").concat(question));
    answersDiv.removeChild(answerToRemove);
    answersDiv.removeChild(closeImageToRemove);

    showOrHideRemoveAnswer(question);
}