function drawTable(data, tableId, timeStampFormat) {
    for (var i = 0; i < data.length; i++) {
        drawRow(data[i], tableId, timeStampFormat);
    }
}

function drawRow(rowData, tableId, timeStampFormat) {
    var row = $("<tr />");
    $(tableId).append(row);
    row.append($("<td>" + rowData.Size + "</td>"));
    row.append($("<td>" + moment(rowData.Timestamp).format(timeStampFormat) + "</td>"));
    row.append($("<td>" + "<a class=\"btn btn-default\" href=\"/Video/" + rowData.EncodedFilename + "\">Download</a>" + "</td>"));
}

function populateTable(data, tableId, timeStampFormat) {
    data.sort(function (a, b) {
        return (a["Timestamp"] > b["Timestamp"]) ? 1 : ((a["Timestamp"] < b["Timestamp"]) ? -1 : 0);
    });
    drawTable(data, tableId, timeStampFormat);
}