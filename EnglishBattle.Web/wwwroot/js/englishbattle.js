function EnglishBattle(gameId, verbList) {

    __this = this;
    _this.gameId = gameId;

    _this.score = 0;

    _this.inProgress = false;
    _this.infiniteLife = false;

    _this.verbList = verbList;

    _this.firstTimestamp = "";

    _this.verbIndex = 0;
    _this.hintIndex = 1;
}

EnglishBattle.prototype.Start = function (timestamp) {

    _this.inProgress = true;

    $.ajax({
        type: "PUT",
        url: "/EnglishBattle?handler=Start",
        data: {
            gameId: _this.gameId,
            startedAt: timestamp
        },
        dataType: "json",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            console.log(data);
        },
        error: function (err) {
            console.log(err);
        }
    });
}

EnglishBattle.prototype.PostAnswer = function (preterit, pastPrinciple, timestamp) {
    var currentVerb = _this.GetCurrentVerb();

    $.ajax({
        type: "POST",
        url: "/EnglishBattle?handler=Answer",
        data: {
            answer: {
                verbId: currentVerb.id,
                gameId: _this.gameId,
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
            console.log(data);
        },
        error: function (err) {
            console.log(err);
        }
    });
}

EnglishBattle.prototype.GetCurrentVerb = function () {
    if (_this.verbIndex - 1 < 0) {
        return null;
    }

    return _this.verbList[_this.verbIndex - 1];
}

EnglishBattle.prototype.GetNextVerb = function () {
    if (_this.verbIndex > _this.verbList.length) {
        return null;
    }

    return _this.verbList[_this.verbIndex++].baseForm;
}

EnglishBattle.prototype.GetHints = function () {
    var hints = [];

    for (; _this.hintIndex < 4; _this.hintIndex++) {
        hints.push(_this.verbList[_this.hintIndex].baseForm);
    }

    return hints;
}

EnglishBattle.prototype.GetNextHint = function () {
    if (_this.hintIndex >= _this.verbList.length) {
        return null;
    }

    return _this.verbList[_this.hintIndex++].baseForm
}