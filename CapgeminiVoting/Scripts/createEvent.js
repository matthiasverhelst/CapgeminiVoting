$(document).ready(function () {
    showOrHideRemoveQuestion();

    for (var i = 0; i <= viewBag.questionCount; i++)
    {
        showOrHideRemoveAnswer(i);
    }
	
	$("#Name").keyup(function() {
		enableOrDisableSubmitButton();
	});

	enableOrDisableSubmitButton();
	bindQuestionTypeTrigger();
});

function bindQuestionTypeTrigger()
{
    $(".questionType").change(function () {
        if ($(this).val() == "2") {
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
	if($("#Name").val() == "") {
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
    if ($(answersDiv.concat(" > input")).length > 4) {
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

	var hiddenInput = document.createElement("input");
	hiddenInput.setAttribute("type", "hidden");
	hiddenInput.setAttribute("id", questionsIndex);
	hiddenInput.setAttribute("name", "Questions.Index");
	hiddenInput.setAttribute("value", questionsIndex);

	var questionInput = document.createElement("input");
	questionInput.setAttribute("type", "text");
	questionInput.setAttribute("id", ("Questions[").concat(questionsIndex).concat("].Question"));
	questionInput.setAttribute("name", ("Questions[").concat(questionsIndex).concat("].Question"));
	questionInput.setAttribute("placeholder", "Enter your question");
	questionInput.setAttribute("class", "form-control");

	question.appendChild(questionLabel);
	question.appendChild(hiddenInput);
	question.appendChild(questionInput);
	
	var questionType = document.createElement("div");
	questionType.setAttribute("class", "form-group");

	var questionTypeLabel = document.createElement("label");
	questionTypeLabel.setAttribute("for", "Type");
	questionTypeLabel.innerHTML = "Type";

	var selectQuestionType = document.createElement("select");
	selectQuestionType.setAttribute("id", ("Questions[").concat(questionsIndex).concat("].QuestionType"));
	selectQuestionType.setAttribute("name", ("Questions[").concat(questionsIndex).concat("].QuestionType"));
	selectQuestionType.setAttribute("class", "form-control questionType");

	var multipleChoice = document.createElement("option");
	multipleChoice.setAttribute("value", "0");
	multipleChoice.innerHTML = "Multiple choice";
	var checkboxes = document.createElement("option");
	checkboxes.setAttribute("value", "1");
	checkboxes.innerHTML = "Checkboxes";
	var textnumber = document.createElement("option");
	textnumber.setAttribute("value", "2");
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

	var hiddenInput1 = document.createElement("input");
	hiddenInput1.setAttribute("type", "hidden");
	hiddenInput1.setAttribute("id", "Questions[".concat(question).concat("].Answers[0].Index"));
	hiddenInput1.setAttribute("name", "Questions[".concat(questionsIndex).concat("].Answers.Index"));
	hiddenInput1.setAttribute("value", "0");

	var answerInput1 = document.createElement("input");
	answerInput1.setAttribute("type", "text");
	answerInput1.setAttribute("id", ("Questions[").concat(questionsIndex).concat("].Answers[0].Answer"));
	answerInput1.setAttribute("name", ("Questions[").concat(questionsIndex).concat("].Answers[0].Answer"));
	answerInput1.setAttribute("placeholder", "Enter your answer");
	answerInput1.setAttribute("class", "form-control answer");
	
	var removeAnswer1 = document.createElement("img");
	removeAnswer1.setAttribute("src", "/Images/close.png");
	removeAnswer1.setAttribute("alt", "Remove this answer");
	removeAnswer1.setAttribute("id", "removeAnswer".concat(questionsIndex).concat("0"));
	removeAnswer1.setAttribute("class", "removeAnswer removeAnswer".concat(questionsIndex));
	removeAnswer1.setAttribute("onclick", ("removeAnswer(").concat(questionsIndex).concat(", 0)"));

	var hiddenInput2 = document.createElement("input");
	hiddenInput2.setAttribute("type", "hidden");
	hiddenInput2.setAttribute("id", "Questions[".concat(question).concat("].Answers[1].Index"));
	hiddenInput2.setAttribute("name", "Questions[".concat(questionsIndex).concat("].Answers.Index"));
	hiddenInput2.setAttribute("value", "1");

	var answerInput2 = document.createElement("input");
	answerInput2.setAttribute("type", "text");
	answerInput2.setAttribute("id", ("Questions[").concat(questionsIndex).concat("].Answers[1].Answer"));
	answerInput2.setAttribute("name", ("Questions[").concat(questionsIndex).concat("].Answers[1].Answer"));
	answerInput2.setAttribute("placeholder", "Enter your answer");
	answerInput2.setAttribute("class", "form-control answer");

	var removeAnswer2 = document.createElement("img");
	removeAnswer2.setAttribute("src", "/Images/close.png");
	removeAnswer2.setAttribute("alt", "Remove this answer");
	removeAnswer1.setAttribute("id", "removeAnswer".concat(questionsIndex).concat("1"));
	removeAnswer2.setAttribute("class", "removeAnswer removeAnswer".concat(questionsIndex));
	removeAnswer2.setAttribute("onclick", ("removeAnswer(").concat(questionsIndex).concat(", 1)"));

	div.appendChild(answerLabel);
	div.appendChild(hiddenInput1);
	div.appendChild(answerInput1);
	div.appendChild(removeAnswer1);
	div.appendChild(hiddenInput2);
	div.appendChild(answerInput2);
	div.appendChild(removeAnswer2);
	
	var addAnswerDiv = document.createElement("div");
	addAnswerDiv.setAttribute("class", "form-group addAnswerDiv");

	var addAnswer = document.createElement("input");
	addAnswer.setAttribute("type", "button");
	addAnswer.setAttribute("value", "Add answer");
	addAnswer.setAttribute("id", "addAnswer".concat(questionsIndex));
	addAnswer.setAttribute("class", "btn btn-primary");
	addAnswer.setAttribute("onclick", "addAnswer(".concat(questionsIndex).concat(", 2)"));
	
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
    var hiddenInput = document.createElement("input");
    hiddenInput.setAttribute("type", "hidden");
    hiddenInput.setAttribute("id", "Questions[".concat(question).concat("].Answers[").concat(answer).concat("].Index"));
    hiddenInput.setAttribute("name", "Questions[".concat(question).concat("].Answers.Index"));
    hiddenInput.setAttribute("value", answer);

	var answerInput = document.createElement("input");
	answerInput.setAttribute("type", "text");
	answerInput.setAttribute("id", (("Questions[").concat(question).concat("].Answers[").concat(answer).concat("].Answer")));
	answerInput.setAttribute("name", (("Questions[").concat(question).concat("].Answers[").concat(answer).concat("].Answer")));
	answerInput.setAttribute("placeholder", "Enter your answer");
	answerInput.setAttribute("class", "form-control answer");

	var removeAnswer = document.createElement("img");
	removeAnswer.setAttribute("src", "/Images/close.png");
	removeAnswer.setAttribute("alt", "Remove this answer");
	removeAnswer.setAttribute("id", "removeAnswer".concat(question).concat(answer));
	removeAnswer.setAttribute("class", "removeAnswer removeAnswer".concat(question));
	removeAnswer.setAttribute("onclick", "removeAnswer(".concat(question).concat(",").concat(answer).concat(")"));
	
	document.getElementById(("answersDiv").concat(question)).appendChild(hiddenInput)
	document.getElementById(("answersDiv").concat(question)).appendChild(answerInput);
	document.getElementById(("answersDiv").concat(question)).appendChild(removeAnswer);
	document.getElementById(("addAnswer").concat(question)).onclick = function () { addAnswer(question, (answer + 1)); };

	showOrHideRemoveAnswer(question);
}

function removeQuestion(question)
{
    var questionToRemove = document.getElementById(("questionDiv").concat(question));
    var questionsDiv = document.getElementById("questionsDiv");

    questionsDiv.removeChild(questionToRemove);

	showOrHideRemoveQuestion();
}

function removeAnswer(question, answer)
{
    var answersDiv = document.getElementById(("answersDiv").concat(question));
    var answerToRemove = document.getElementById(("Questions[").concat(question).concat("].Answers[").concat(answer).concat("].Answer"));
    var hiddenInputToRemove = document.getElementById(("Questions[").concat(question).concat("].Answers[").concat(answer).concat("].Index"));
    var closeImageToRemove = document.getElementById(("removeAnswer").concat(question).concat(answer));
    
    answersDiv.removeChild(answerToRemove);
    answersDiv.removeChild(hiddenInputToRemove);
    answersDiv.removeChild(closeImageToRemove);

    showOrHideRemoveAnswer(question);
}