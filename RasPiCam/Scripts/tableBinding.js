function drawTable(data, tableId, timeStampFormat) {
    for (var i = 0; i < data.length; i++) {
        drawRow(data[i], tableId, timeStampFormat);
    }
}

function drawRow(rowData, tableId, timeStampFormat) {
    var row = $("<tr />");
    $(tableId).append(row);
    row.append($("<td>" + "<a href=\"/Video/" + rowData.EncodedFilename + "\">" + rowData.Filename + "</a>" + "</td>"));
    row.append($("<td>" + rowData.Size + "</td>"));
    row.append($("<td>" + moment(rowData.Timestamp).format(timeStampFormat) + "</td>"));
}

function populateTable(data, tableId, timeStampFormat) {
    data.sort(function (a, b) {
        return (a["Timestamp"] > b["Timestamp"]) ? 1 : ((a["Timestamp"] < b["Timestamp"]) ? -1 : 0);
    });
    drawTable(data, tableId, timeStampFormat);
}