$('.input__file').on('change', function (event) {
    event.preventDefault();

    let format = $('.input__file').val().split('.')[1]
    if (format === "txt" || format === "docx"){
        $.ajax({
            url: '/home/uploadfile',
            type: 'POST',
            data: new FormData($('.upload-file__form')[0]),
            contentType: false,
            processData: false,
            success: function (data) {
                $('#input__text').val(data);
            }
        });
    }
});


/*function getJsonForm(formName) {
    let form = document.formsp[formName];
    let fd = new FormData(form);

    let data = {};
    for (let [key, prop] of fd) {
        data[key] = prop;
    }

    return JSON.stringify(data, null, 2);
}

async function encodeText() {
            let requestData = JSON.stringify({
                key: key.value,
                text: text.value
            })

            let response = await fetch('https://localhost:44384/home/crypt/', {method: 'POST', body: requestData}
            output.innerHTML = response.json().encodedText
}

$('.crypt__btn').on('click', async function (event) {
    event.preventDefault();

    var json = getJsonForm('input__form');
    console.log(json)

   $.ajax({
        url: '/home/input',
        type: 'POST',
        data: json,
        contentType: false,
        processData: false,
        success: async function (data) {
            $('.output').html(data);
        }
   });
});*/

/*$('.save__btn').on('click', async function encodeText() {
    let requestData = JSON.stringify({
        text: text__output.value,
        filename: save__filename.value
    })

    await fetch('/home/Download', {
        method: 'POST',
        body: requestData
    })
});*/