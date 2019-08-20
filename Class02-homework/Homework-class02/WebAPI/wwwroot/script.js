let firstname = document.getElementById("firstname");
let lastname = document.getElementById("lastname");
let age = document.getElementById("age");
let btn = document.getElementById("submit");

let getUsersBtn = document.getElementById("getUsers");
let list = document.getElementById("usersList");

let port = "64969";
let url = "http://localhost:" + port + "/api/users";

btn.addEventListener("click", async () => {
    let url = "http://localhost:" + port + "/api/users";

    let body = {
        firstname: firstname.value,
        lastname: lastname.value,
        age: age.value
    }

    await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body)
        
    });
})

getUsersBtn.addEventListener("click", async () => {
    let response = await fetch(url);
    let data = await response.json();
    console.log(data);
    list.append(data.map(x => `${x.firstname} ${x.lastname} - ${x.age} || `));
       
})

