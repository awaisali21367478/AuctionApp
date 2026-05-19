const hubUrl =
    document.getElementById("hubUrl").value;

const connection = new signalR.HubConnectionBuilder()
    .withUrl(hubUrl)
    .build();

connection.on("BidUpdated", () => {

    location.reload();

});

connection.start();

async function placeBid() {

    try {

        const amount =
            document.getElementById("bidAmount").value;

        const email =
            document.getElementById("userEmail").value;

        if (!amount) {

            document.getElementById("message").innerText =
                "Please enter bid amount";

            return;
        }

        const apiUrl =
            document.getElementById("bidApiUrl").value;

        const response = await fetch(apiUrl, {

            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify({
                email,
                amount
            })

        });

        const result = await response.json();

        const message =
            document.getElementById("message");

        message.innerText = result.message;

        if (result.success) {

            message.className =
                "message text-success";
        }
        else {

            message.className =
                "message text-danger";
        }

    }
    catch {

        document.getElementById("message").innerText =
            "Something went wrong";

    }
}