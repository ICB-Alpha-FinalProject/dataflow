
/* ADD SENSOR ID TO THE <td> ELEMENT U WANT TO UPDATE */
function startSignalRUpdate(data) {

    let update = $.connection.sensorUpdate;
    $.connection.hub.start().done(function () {
        setInterval(function () {

            update.server.send(data)
        }, 1000);

    });

    update.client.updateChart =
        function (sensorsUpdate) {
            sensorsUpdate = JSON.parse(sensorsUpdate);
            //console.log(sensorsUpdate);
            for (let update of sensorsUpdate) {
                let id = update.Id;

                $(`td#${id}`).html(parseSpan(update.Value, update.MinValue,
                    update.MaxValue, update.IsValueType))
            }
        }
}

function parseSpan(currValue, min, max, isValue) {

    let classVal = "";
    let span = "";
    if (isValue) {
        if (currValue < min) {
            classVal = "fa fa-exclamation-triangle text-primary";
        } else if (currValue > max) {
            classVal = "text-danger fa fa-exclamation-triangle";
        } else {
            classVal = "text-success";
        }
        span = `<span class="${classVal}">${currValue}</span>`

    }
    else {
        classVal = currValue === "False" ? "fa fa-window-close" : "fa fa-check";
        span = `<span class="${classVal}"></span>`

    }
    return span;
}