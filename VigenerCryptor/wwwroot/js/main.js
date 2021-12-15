function getJsonForm(formName) {
    let form = document.formsp[formName];
    let fd = new FormData(form);

    let data = {};
    for (let [key, prop] of fd) {
        data[key] = prop;
    }

    return JSON.stringify(data, null, 2);
}


$('.crypt__btn').on('click', async function (event) {
    event.preventDefault();

    var json = getJsonForm('input__form');
    console.log(json)

   $.ajax({
        url: '/home/input',
        type: 'POST',
        data: json,
        cache: false,
        contentType: false,
        processData: false,
        success: async function (data) {
            $('.output').html(data);
        }
   });
});