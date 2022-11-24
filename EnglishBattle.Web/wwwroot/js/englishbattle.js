"use strict";

function EnglishBattle(playerId) {

    this.playerId = playerId;
    this.gameId = 0;

    this.score = 0;

    this.inProgress = false;

    this.verbList = null;

    this.firstTimestamp = "";
    this.lastTimestamp = "";

    this.verbIndex = 0;
    this.hintIndex = 1;
}

EnglishBattle.prototype.SetGameId = function (gameId) {
    this.gameId = gameId;
}

EnglishBattle.prototype.SetVerbs = function (verbs) {
    this.verbList = verbs;
    this.verbIndex = 0;
    this.hintIndex = 1;
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