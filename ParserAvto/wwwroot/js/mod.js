function sendUserNameAndOpenModal() {
    const userName = document.getElementById('user-data').dataset.username;

    const data = { username: userName };
    const xhr = new XMLHttpRequest();
    xhr.open('POST', '/User/GetUser', true);
    xhr.setRequestHeader('Content-Type', 'application/json');

    xhr.onload = function () {
        if (xhr.status === 200) {
            const response = JSON.parse(xhr.responseText);
            openModal(response.username);
        } else {
            console.error('Ошибка при отправке запроса на сервер');
        }
    };

    xhr.send(JSON.stringify(data));
}

// Функция для открытия модального окна и передачи информации о пользователе
function openModal(username) {
    document.getElementById("userName").textContent = username;
    document.getElementById("myModal").style.display = "block";
}
function submitData() {
    const userName = document.getElementById('userName').textContent;
    const userNumber = document.getElementById('userNumber').value;

    const data = { username: userName, number: userNumber };
    const xhr = new XMLHttpRequest();
    xhr.open('POST', '/User/Submit', true);
    xhr.setRequestHeader('Content-Type', 'application/json');

    xhr.onload = function () {
        if (xhr.status === 200) {
            closeModal();
        } else {
            console.error('Ошибка при отправке данных на сервер');
        }
    };

    xhr.send(JSON.stringify(data));

 
}

// Функция для закрытия модального окна
function closeModal() {
    document.getElementById("myModal").style.display = "none";
}
// Функция для закрытия модального окна
