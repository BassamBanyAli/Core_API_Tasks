async function login(event) {
    event.preventDefault();
    var url = 'https://localhost:7167/api/Users/login';
    var form = document.getElementById("form");
    var formData = new FormData(form);

    var response = await fetch(url, {
        method: "POST",
        body: formData,
    });
     if(response.status==200)
    alert('logged sucessfully');
else
alert('Unotharized');
    
}