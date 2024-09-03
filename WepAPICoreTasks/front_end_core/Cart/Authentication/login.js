async function login(event) {
    event.preventDefault();
    var url = 'https://localhost:7167/api/Users/login';
    var form = document.getElementById("form");
    var formData = new FormData(form);

    var response = await fetch(url, {
        method: "POST",
        body: formData,
    });
    var result= await response.json();
     if(response.ok){
        localStorage.setItem('jwtToken', result.token);
    alert('logged sucessfully');
}
else
alert('Unotharized');
    
}