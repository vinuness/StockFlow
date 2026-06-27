const cadastrar = document.getElementById("cadastrar");

const cep = document.getElementById("cep");
const estado = document.getElementById("estado");
const cidade = document.getElementById("cidade");
const bairro = document.getElementById("bairro");
const rua = document.getElementById("rua"); 

cep.addEventListener("focusout", async () => {
    try {
        const soNumero = /^[0-9]+$/; //regex para colocar apenas numeros
        const cepValido = /^[0-9]{8}$/; //regex para validar cep

        if (!soNumero.test(cep.value) || !cepValido.test(cep.value)) { //se nao tiver so numero ou cep valido dá erro
            throw new Error("CEP inválido");
        }

        const response = await fetch(`https://viacep.com.br/ws/${cep.value}/json/`); //faz requisicao para API do viacep

        if (!response.ok) {
            throw new Error("Erro ao consultar o CEP.");
        }

        const data = await response.json(); //pega as informações da resposta

        if (data.erro) {
            throw new Error("CEP não encontrado.");
        }

        //insere nos campos
        estado.value = data.uf;
        cidade.value = data.localidade;
        bairro.value = data.bairro;
        rua.value = data.logradouro;

    } catch (error) {
        alert(error.message);

        estado.value = "";
        cidade.value = "";
        bairro.value = "";
        rua.value = "";
        numero.value = "";
    }
});

cadastrar.addEventListener("click", (e) => {

    const usuarioCadastrado = {
        cpf: document.getElementById("cpf").value,
        nome: document.getElementById("nome").value,
        email: document.getElementById("email").value,
        senha: document.getElementById("senha").value,
        cep: cep.value,
        estado: estado.value,
        cidade: cidade.value,
        bairro: bairro.value,
        rua: rua.value,
        numero: document.getElementById("numero").value
    };

    if (
        usuarioCadastrado.cpf &&
        usuarioCadastrado.nome &&
        usuarioCadastrado.email &&
        usuarioCadastrado.senha &&
        usuarioCadastrado.cep &&
        usuarioCadastrado.estado &&
        usuarioCadastrado.cidade &&
        usuarioCadastrado.bairro &&
        usuarioCadastrado.rua &&
        usuarioCadastrado.numero
    ) {
        alert("Cadastro efetuado com sucesso!");
    } else {
        alert("Preencha todos os campos obrigatórios.");
    }
});