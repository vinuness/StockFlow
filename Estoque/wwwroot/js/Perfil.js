document.addEventListener("DOMContentLoaded", async () => {

    const usuarioLocal = JSON.parse(localStorage.getItem("usuario"));

    if (!usuarioLocal) {
        alert("Você não está logado");
        window.location.href = "/Cliente/Logar";
        return;
    }

    const res = await fetch(`https://localhost:7238/api/Cliente/findById/${usuarioLocal.id}`);

    if (!res.ok) {
        alert("Erro ao carregar perfil");
        return;
    }

    const cliente = await res.json();

    document.getElementById("id").innerText = cliente.id;
    document.getElementById("nome").innerText = cliente.nome;
    document.getElementById("email").innerText = cliente.email;

    document.getElementById("cep").innerText = cliente.endereco?.cep ?? "-";
    document.getElementById("rua").innerText = cliente.endereco?.rua ?? "-";
    document.getElementById("bairro").innerText = cliente.endereco?.bairro ?? "-";
    document.getElementById("cidade").innerText = cliente.endereco?.cidade ?? "-";
    document.getElementById("estado").innerText = cliente.endereco?.estado ?? "-";

});