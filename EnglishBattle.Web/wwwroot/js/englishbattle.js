function EnglishBattle(idPartie, niveau, baseVerbale, reponseP, reponsePP) {

    _this = this;
    this.idPartie = idPartie;
    this.niveau = niveau;

    this.score = 0;

    this.inProgress = false;
    this.infiniteLife = false;

    this.wordList = [];

    this.baseVerbale = baseVerbale  // DOM input element
    this.reponseP = reponseP;       // DOM input element
    this.reponsePP = reponsePP;     // DOM input element
    // timestamps de reception/envoi des v
    this.firstTimestamp = "";

    this.wordIndex = 0;
    this.hintIndex = 1;
}

EnglishBattle.prototype.Shuffle = function (array) {

    var currentIndex = array.length;
    var tempValue;
    var randomIndex;

    while (0 !== currentIndex) {
        randomIndex = Math.floor(Math.random() * currentIndex);
        currentIndex -= 1;

        tempValue = array[currentIndex];
        array[currentIndex] = array[randomIndex];
        array[randomIndex] = tempValue;
    }

    return (array);
}

EnglishBattle.prototype.Init = function (verbesJSON) {

    var tmp = JSON.parse(verbesJSON);
    this.wordList = this.Shuffle(tmp);
}

EnglishBattle.prototype.Start = function (timestamp) {

    this.inProgress = true;
    // Affichage du mot à trouver
    this.baseVerbale.val(this.wordList[this.wordIndex++].baseVerbale);
    // Affichage des 3 prochains mots
    for (; this.hintIndex <= 3; this.hintIndex++) {
        this.NextHint(this.wordList[this.hintIndex].baseVerbale);
    }
    // Le jeu a commencé a ce point, récupération du premier timestamp pour la question #1
    this.firstTimestamp = timestamp;
}

EnglishBattle.prototype.GetReponse = function (lastTimestamp) {

    var reponse = JSON.stringify({
        reponse: {
            idPartie: this.idPartie,
            idVerbe: this.GetCurrentVerbe().id,
            //PP et P inversés car ils le sont également en base
            reponsePreterit: this.reponsePP.val(),
            reponseParticipePasse: this.reponseP.val(),
            dateEnvoie: this.firstTimestamp,
            dateReponse: lastTimestamp
        }
    });

    return (reponse);
}

// Affiche le verbe suivant en prenant en parametre l'élément html qui affiche le verbe
EnglishBattle.prototype.NextWord = function () {
    // S'il n'y a plus de mot, retourne false pour signifier la fin de la partie
    if (this.wordIndex >= this.wordList.length)
        return (false);

    var nextWord = " ";
    var nextHint = "&nbsp";
    // Selection du prochain mot
    if (this.wordIndex < this.wordList.length) {
        nextWord = this.wordList[this.wordIndex++].baseVerbale;
    }

    // Affectation de la nouvelle valeur de base verbale
    this.baseVerbale.val(nextWord);

    // Récupération du prochain hint
    if (this.hintIndex < this.wordList.length)
        nextHint = this.wordList[this.hintIndex++].baseVerbale
    this.NextHint(nextHint);

    return (true);
}

// Ajoute un nouveau verbe à la liste des prochains verbes qui vont sortir
EnglishBattle.prototype.NextHint = function (nextHint) {
    var toPrepend = `<div class="" style="opacity: 0.3;">${nextHint}</div>`;
    // Ajout de la ligne avec une animation d'entrée en fondu
    $(toPrepend).hide().prependTo($("#next-words")).fadeIn();
}

// Retourne l'id du verbe à renvoyer au serveur
EnglishBattle.prototype.GetCurrentVerbe = function () {
    var verbe = this.wordList[this.wordIndex - 1];
    return (verbe);
}

// Check si les deux champs de réponse ne sont pas vide
EnglishBattle.prototype.ReponseNotEmpty = function () {

    var p = this.reponseP.val();
    var pp = this.reponsePP.val();

    if (p == "" || pp == "") {
        // Met en evidence les inputs vides
        if (p == "") {
            this.reponseP.addClass("highlight-error");
        }
        if (pp == "") {
            this.reponsePP.addClass("highlight-error");
        }
        return (false);
    }
    return (true);
}

// Nettoye les deux champs de réponse, et place le curseur sur la première réponse
EnglishBattle.prototype.ClearInputs = function () {

    this.reponseP.val("");
    this.reponsePP.val("");
    this.reponseP.focus();
}

EnglishBattle.prototype.TimeOut = function (timestamp) {

    //console.log("timer out");
    this.inProgress = false;
    setTimeout(function () { __doPostBack("GAME_OVER", timestamp); }, 1);
}

EnglishBattle.prototype.UpdateScore = function (scoreHtmlElement) {
    if (this.inProgress) {
        this.score++;
        scoreHtmlElement.html(this.score);
    }
}

EnglishBattle.prototype.SetInfinite = function () {
    this.infiniteLife = true;
}