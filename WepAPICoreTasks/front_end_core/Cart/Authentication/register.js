async function Register(event) {
    event.preventDefault();
    var url = 'https://localhost:7167/api/Users/register';
    var form = document.getElementById("form");
    var formData = new FormData(form);

    var response = await fetch(url, {
        method: "POST",
        body: formData,
    });
     if(response.ok){
    alert('Register sucessfully');
window.location.href="login.html";
}
else
alert('erorr');
    
}