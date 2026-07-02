const cadastrar = document.getElementById("cadastrar");

cadastrar.addEventListener("click", (e) => {

    const usuarioCadastrado = {
        cpf: document.getElementById("cpf").value,
        nome: document.getElementById("nome").value,
        email: document.getElementById("email").value,
        senha: document.getElementById("senha").value,
    };

    if (
        usuarioCadastrado.cpf &&
        usuarioCadastrado.nome &&
        usuarioCadastrado.email &&
        usuarioCadastrado.senha &&
    ) {
        alert("Cadastro efetuado com sucesso!");
    } else {
        alert("Preencha todos os campos obrigatórios.");
    }
});