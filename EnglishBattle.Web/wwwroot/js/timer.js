// Code stoled from internet:
// https://jsfiddle.net/prafuitu/xRmGV/

function Timer(seconds = 60) {
    _this = this;
    _this.timer;
    _this.duration = seconds;
    _this.tick = 16;
    _this.timePenalty = 0;

    _this.endCallBack = null;

    _this.radius = 60;
    _this.offset = 10;

    _this.col_H = 120;
    _this.col_S = 95;
    _this.col_L = 48;

    _this.miliseconds = null;

    for (var x = 0; x < 24; x++) {
        var path = document.createElementNS('http://www.w3.org/2000/svg', 'path');

        path.setAttributeNS(null, 'd', 'M70,70 L70,10 A60,60 0 0,1 77.3121,10.4472');
        path.setAttributeNS(null, 'fill', '#333');
        path.setAttributeNS(null, 'fill-opacity', 0.3);
        path.setAttributeNS(null, 'stroke', '#333');
        path.setAttributeNS(null, 'stroke-width', 1);
        path.setAttributeNS(null, 'stroke-opacity', 0.4);
        path.setAttributeNS(null, 'transform', 'rotate(' + ((x * 15) - 4) + ' 70 70)');
    }
}

// Timestamp format: 'YYYY-MM-DD HH:mm:ss'
Timer.prototype.getTimeStamp = function () {
    var today = new Date();

    var month = (today.getMonth() + 1) < 10 ? `0${today.getMonth() + 1}` : (today.getMonth() + 1);
    var date = `${today.getFullYear()}-${month}-${today.getDate()}`;
    var time = `${today.getHours()}:${today.getMinutes()}:${today.getSeconds()}`;

    return (`${date} ${time}`);
}

Timer.prototype.hue2rgb = function (t1, t2, t3) {
    if (t3 < 0) t3 += 1;
    if (t3 > 1) t3 -= 1;

    if (t3 * 6 < 1) return t2 + (t1 - t2) * 6 * t3;
    if (t3 * 2 < 1) return t1;
    if (t3 * 3 < 2) return t2 + (t1 - t2) * (2 / 3 - t3) * 6;

    return t2;
}

Timer.prototype.hsl2rgb = function (H, S, L) {
    var R, G, B;
    var t1, t2;

    H = H / 360;
    S = S / 100;
    L = L / 100;

    if (S == 0) {
        R = G = B = L;
    } else {
        t1 = (L < 0.5) ? L * (1 + S) : L + S - L * S;
        t2 = 2 * L - t1;

        R = _this.hue2rgb(t1, t2, H + 1 / 3);
        G = _this.hue2rgb(t1, t2, H);
        B = _this.hue2rgb(t1, t2, H - 1 / 3);
    }

    return [
        Math.round(R * 255),
        Math.round(G * 255),
        Math.round(B * 255)
    ];
}

// Progression du timer
Timer.prototype.drawCoord = function (radius, degrees) {
    var radians = degrees * Math.PI / 180;

    var rX = radius + _this.offset + Math.sin(radians) * radius;
    var rY = radius + _this.offset - Math.cos(radians) * radius;

    var dir = (degrees > 180) ? 1 : 0;

    var coord = 'M' + (radius + _this.offset) + ',' + (radius + _this.offset) + ' ' +
        'L' + (radius + _this.offset) + ',' + _this.offset + ' ' +
        'A' + radius + ',' + _this.radius + ' 0 ' + dir + ',1 ' +
        rX + ',' + rY;

    return coord;
}

Timer.prototype.update = function (deg, sec) {
    var RGB = [];
    var draw = _this.drawCoord(_this.radius, deg);

    $('#progress_3').attr('d', draw);

    _this.col_H = 120 - Math.round(deg / 3);
    RGB = _this.hsl2rgb(_this.col_H, _this.col_S, _this.col_L);

    $('#progress_3').attr('fill', 'rgb(' + RGB.join(', ') + ')');
    $('#seconds_3').html(sec);
}

Timer.prototype.updateTimer = function () {
    var date = new Date();

    if (_this.miliseconds == null) _this.miliseconds = date.getTime();

    var diff = (date.getTime() - _this.miliseconds) % (1000 * _this.duration);

    if (diff + (_this.tick * 2) >= _this.duration * 1000) {
        _this.stop();
        $('#progress_3').attr('d', "0");
        $('#seconds_3').html("0");

        _this.endCallBack(_this.getTimeStamp());

        return;
    }

    var degrees = 0.36 * diff / _this.duration;
    var seconds = _this.duration - Math.floor(diff / 1000);

    _this.update(degrees, seconds);
}

Timer.prototype.start = function (endCallBack) {
    _this.endCallBack = endCallBack;
    _this.timer = setInterval(_this.updateTimer, _this.tick);
}

Timer.prototype.stop = function () {
    clearInterval(_this.timer);
    _this.miliseconds = null;
}

Timer.prototype.refresh = function () {
    _this.stop();
    _this.start();
}

Timer.prototype.reset = function () {
    _this.stop();
    _this.miliseconds = null;
    _this.start();
}

Timer.prototype.addSeconds = function (seconds) {
    if (_this.duration + seconds <= 0) {
        _this.stop();
        $('#progress_3').attr('d', "0");
        $('#seconds_3').html("0");

        _this.endCallBack(_this.getTimeStamp());
    }

    _this.duration += seconds;
}