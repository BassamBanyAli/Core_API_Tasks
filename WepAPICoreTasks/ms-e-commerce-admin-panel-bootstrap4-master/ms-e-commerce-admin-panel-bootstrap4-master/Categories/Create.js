async function CreateCategory(event) {
    event.preventDefault();
     var url=`https://localhost:44323/api/Categories`;
     var form=document.getElementById("form");
     var formData=new FormData(form);


     var response=await fetch(url,{
        method:"POST",
        body:formData,
     });

     var data=response.json();
     alert('Data stored in local storage');
     window.location.href="../index.html";

}