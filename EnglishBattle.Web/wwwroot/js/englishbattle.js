"use strict";

function EnglishBattle(gameId, verbList) {

    this.gameId = gameId;

    this.score = 0;

    this.inProgress = false;
    this.infiniteLife = false;

    this.verbList = verbList;

    this.firstTimestamp = "";
    this.lastTimestamp = "";

    this.verbIndex = 0;
    this.hintIndex = 1;
}

EnglishBattle.prototype.Timeout = function () {
    this.inProgress = false;
    this.lastTimestamp = this.timer.getTimeStamp();
}

EnglishBattle.prototype.NewGame = function (timestamp) {

    this.inProgress = true;
    this.firstTimestamp = timestamp;

    $.ajax({
        type: "PUT",
        url: "/EnglishBattle?handler=Start",
        data: {
            gameId: this.gameId,
            startedAt: timestamp
        },
        dataType: "json",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            console.log("start success");
        },
        error: function (err) {
            console.log(err);
            console.log("start error");
        }
    });
}

EnglishBattle.prototype.PostAnswer = function (preterit, pastPrinciple, timestamp) {
    var currentVerb = this.GetCurrentVerb();

    $.ajax({
        type: "POST",
        url: "/EnglishBattle?handler=Answer",
        data: {
            answer: {
                verbId: currentVerb.id,
                gameId: this.gameId,
                preterit: preterit,
                pastPrinciple: pastPrinciple,
                answeredAt: timestamp
            }
        },
        dataType: "json",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            console.log("posted answer successfully")
        },
        error: function (err) {
            console.log("error while posting answer");
        }
    });
}

EnglishBattle.prototype.IsValidAnswer = function (pastSimple, pastParticiple) {
    let verb = this.GetCurrentVerb();

    if (verb.pastSimple.localeCompare(pastSimple, undefined, { sensitivity: "accent" }) !== 0) {
        return false;
    }
    else if (verb.pastParticiple.localeCompare(pastParticiple, undefined, { sensitivity: "accent" }) !== 0) {
        return false;
    }

    return true;
}

EnglishBattle.prototype.GetCurrentVerb = function () {
    if (this.verbIndex - 1 < 0) {
        return null;
    }

    return this.verbList[this.verbIndex - 1];
}

EnglishBattle.prototype.GetNextVerb = function () {
    if (this.verbIndex > this.verbList.length) {
        return null;
    }

    return this.verbList[this.verbIndex++];
}

EnglishBattle.prototype.GetHints = function () {
    var hints = [];

    for (; this.hintIndex < 4; this.hintIndex++) {
        hints.push(this.verbList[this.hintIndex].baseForm);
    }

    return hints;
}

EnglishBattle.prototype.GetNextHint = function () {
    if (this.hintIndex >= this.verbList.length) {
        return null;
    }

    return this.verbList[this.hintIndex++].baseForm
}