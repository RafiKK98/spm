let loginForm = document.getElementById("login-form");

let user = {};

loginForm.addEventListener("submit", (e) => {
    e.preventDefault();

    let formData = new FormData(loginForm);

    // console.log("userName:" + formData.get("userName"));
    // console.log("passKey:" + formData.get("passKey"));

    let loginCred = {
        userName: formData.get("userName"),
        passKey: formData.get("passKey")
    };

    let data = {
        name: "morpheus",
        job: "leader"
    };

    console.log(JSON.stringify(loginCred));

    let option = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(loginCred),
    };

    fetch("http://localhost:8080/login", option).then(res => {
        return res.json();
    }).then(res => {
        console.log(res);
        user = res;
    });
})