document.getElementById("formLogin").addEventListener("submit", async (e) => {
    e.preventDefault();

    const res = await fetch("/Cliente/Logar", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            email: document.getElementById("email").value,
            senha: document.getElementById("senha").value
        })
    });

    if (!res.ok) {
        alert("Login inválido");
        return;
    }

    const usuario = await res.json();

    localStorage.setItem("usuario", JSON.stringify(usuario));

    window.location.href = "/Home";
});