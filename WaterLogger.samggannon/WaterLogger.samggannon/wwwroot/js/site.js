// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    const robotSpeak = document.getElementById('robot-buddy');
    const bubble = document.getElementById('speech-bubble');

    const initialMessage = "<i>*Beboop Beboop*</i> Welcome! Glad to see you are making healthy choices. <i>*Beboop Beboop*</i> </br> </br> (Click me to dismiss. To read a random water fact, click me again at anytime.)";
    const waterFacts = [
        "Did you know that water makes up about 60% of the human body?",
        "Drinking water helps maintain the balance of bodily fluids.",
        "Water can help control calories.",
        "Water energizes muscles.",
        "Water helps keep skin looking good.",
        "Water helps your kidneys.",
        "Water maintains normal bowel function."
    ];

    bubble.innerHTML = initialMessage;
    bubble.style.display = 'block';

    let dismissed = false;

    robotSpeak.addEventListener('click', function () {
        if (!dismissed) {
            bubble.style.display = 'none';
            dismissed = true;
        } else {
            const randomFact = waterFacts[Math.floor(Math.random() * waterFacts.length)];
            bubble.innerHTML = randomFact;
            bubble.style.display = 'block';
            dismissed = false;
        }
    });
});
