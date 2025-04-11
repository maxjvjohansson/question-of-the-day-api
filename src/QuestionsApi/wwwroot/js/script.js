document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("questionForm");
    const input = document.getElementById("question");
    const responseBox = document.getElementById("response");

    form.addEventListener("submit", async (e) => {
        e.preventDefault();
        const questionText = input.value.trim();
        
        if (questionText.length < 10 || questionText.length > 250) {
            responseBox.textContent = "Question must be between 10 and 250 characters.";
            return;
        }

        if (!questionText.endsWith("?")) {
            responseBox.textContent = "Question must end with a question mark.";
            return;
        }

        try {
            const response = await fetch("/api/questions", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ text: questionText })
            });

            if (response.ok) {
                responseBox.textContent = "Question submitted successfully!";
                input.value = "";
            } else {
                const error = await response.text();
                responseBox.textContent = `${error}`;
            }
        } catch (err) {
            responseBox.textContent = "An unexpected error occurred.";
            console.error(err);
        }
    });
});