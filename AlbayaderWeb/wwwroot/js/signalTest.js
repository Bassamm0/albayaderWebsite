
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7174/API/ticketHub")
    .build();

connection.on("ReceiveMessage", (user, message) => {
    const msg = message;
        const encodedMsg = user + " :: " + msg;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(err => console.error(err.toString()));

//Send the message

document.getElementById("sendMessage").addEventListener("click", event => {
    const user = document.getElementById("userName").value;
    const message = document.getElementById("userMessage").value;
    connection.invoke("SendMessage", user, message).catch(err => console.error(err.toString()));
    event.preventDefault();
});