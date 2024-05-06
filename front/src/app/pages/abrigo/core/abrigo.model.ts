import { NamedEntity } from "../../../core/entities/named-entity";

export interface Abrigo extends NamedEntity {
  quantidadeNecessariaVoluntarios: number;
  capacidadeTotalPessoas: number;
  quantidadeVagasDisponiveis: number;
  alimentos: Alimento[];
  chavePix: string;
  tipoChavePix: string;
  endereco: Endereco;
}
export interface Alimento {
  nome: string;
  quantidadeNecessaria: number;

}
export interface Endereco {
  rua: string;
  numero: string;
  bairro: string;
  cidade: string;
  cep: string;
  complemento: string;
}
export const abrigos: Abrigo[] = [
  {
    chavePix: '123456789', tipoChavePix: 'CPF',
    endereco: { rua: 'Rua 1', numero: '1', bairro: 'Bairro 1', cidade: 'Cidade 1', cep: '12345678' , complemento: 'Complemento 1'},
    quantidadeNecessariaVoluntarios: 10, capacidadeTotalPessoas: 100, quantidadeVagasDisponiveis: 90,
    alimentos: [
      { nome: 'Arroz', quantidadeNecessaria: 10 },
      { nome: 'Feijão', quantidadeNecessaria: 10 }
    ],
    id: '1',
    name: 'Abrigo 1'
  },
  { chavePix: '123456789', tipoChavePix: 'CPF', endereco: { rua: 'Rua 2', numero: '2', bairro: 'Bairro 2', cidade: 'Cidade 2', cep: '12345678', complemento: 'teste' }, quantidadeNecessariaVoluntarios: 20, capacidadeTotalPessoas: 200, quantidadeVagasDisponiveis: 190, alimentos: [{ nome: 'Arroz', quantidadeNecessaria: 20 }, { nome: 'Feijão', quantidadeNecessaria: 20 }], id: '2', name: 'Abrigo 2' },
  { chavePix: '123456789', tipoChavePix: 'CPF', endereco: { rua: 'Rua 3', numero: '3', bairro: 'Bairro 3', cidade: 'Cidade 3', cep: '12345678', complemento: 'teste' }, quantidadeNecessariaVoluntarios: 30, capacidadeTotalPessoas: 300, quantidadeVagasDisponiveis: 290, alimentos: [{ nome: 'Arroz', quantidadeNecessaria: 30 }, { nome: 'Feijão', quantidadeNecessaria: 30 }], id: '3', name: 'Abrigo 3' },
  { chavePix: '123456789', tipoChavePix: 'CPF', endereco: { rua: 'Rua 4', numero: '4', bairro: 'Bairro 4', cidade: 'Cidade 4', cep: '12345678', complemento: 'teste' }, quantidadeNecessariaVoluntarios: 40, capacidadeTotalPessoas: 400, quantidadeVagasDisponiveis: 390, alimentos: [{ nome: 'Arroz', quantidadeNecessaria: 40 }, { nome: 'Feijão', quantidadeNecessaria: 40 }], id: '4', name: 'Abrigo 4' },
  { chavePix: '123456789', tipoChavePix: 'CPF', endereco: { rua: 'Rua 5', numero: '5', bairro: 'Bairro 5', cidade: 'Cidade 5', cep: '12345678', complemento: 'teste' }, quantidadeNecessariaVoluntarios: 50, capacidadeTotalPessoas: 500, quantidadeVagasDisponiveis: 490, alimentos: [{ nome: 'Arroz', quantidadeNecessaria: 50 }, { nome: 'Feijão', quantidadeNecessaria: 50 }], id: '5', name: 'Abrigo 5' },

];
export enum EStatusCapacidade {
  Lotado,
  Disponivel
}

export interface AbrigoPesquisa {
    id: number;
    nome: string;
    cidade: string;
    bairro: string;
    rua: string;
    numero: number;
    complemento: string;
    tipoChavePix: string;
    chavePix: string;
    capacidade: EStatusCapacidade;
    precisaAjudante: boolean;
    precisaAlimento: boolean;

    capacidadeDesc?: string;
    precisaAjudanteDesc?: string;
    precisaAlimentoDesc?: string;

    capacidadeCssClass?: string;
    precisaAjudanteCssClass?: string;
    precisaAlimentoCssClass?: string;
    enderecoDesc?: string;
}

export interface  AbrigosResult {
    abrigos: AbrigoPesquisa[];
    quantidadeTotalRegistros: number;
}

export const abrigosData: AbrigosResult = {
  abrigos: [
    {
      id: 1,
      nome: "Abrigo 1",
      cidade: "Cidade 1",
      bairro: "Bairro 1",
      rua: "Rua 1",
      numero: 100,
      complemento: "Complemento 1",
      tipoChavePix: "CPF",
      chavePix: "123456789",
      capacidade: EStatusCapacidade.Disponivel,
      precisaAjudante: true,
      precisaAlimento: false
  },
  {
      id: 2,
      nome: "Abrigo 2",
      cidade: "Cidade 2",
      bairro: "Bairro 2",
      rua: "Rua 2",
      numero: 200,
      complemento: "Complemento 2",
      tipoChavePix: "Email",
      chavePix: "abrigo2@email.com",
      capacidade: EStatusCapacidade.Disponivel,
      precisaAjudante: false,
      precisaAlimento: true
  },
  {
      id: 3,
      nome: "Abrigo 3",
      cidade: "Cidade 3",
      bairro: "Bairro 3",
      rua: "Rua 3",
      numero: 300,
      complemento: "Complemento 3",
      tipoChavePix: "Telefone",
      chavePix: "987654321",
      capacidade: EStatusCapacidade.Lotado,
      precisaAjudante: true,
      precisaAlimento: false
  },
  {
      id: 4,
      nome: "Abrigo 4",
      cidade: "Cidade 4",
      bairro: "Bairro 4",
      rua: "Rua 4",
      numero: 400,
      complemento: "Complemento 4",
      tipoChavePix: "CPF",
      chavePix: "98765432100",
      capacidade: EStatusCapacidade.Disponivel,
      precisaAjudante: false,
      precisaAlimento: true
  },
  {
      id: 5,
      nome: "Abrigo 5",
      cidade: "Cidade 5",
      bairro: "Bairro 5",
      rua: "Rua 5",
      numero: 500,
      complemento: "Complemento 5",
      tipoChavePix: "Email",
      chavePix: "abrigo5@email.com",
      capacidade: EStatusCapacidade.Lotado,
      precisaAjudante: true,
      precisaAlimento: false
  },
  {
      id: 6,
      nome: "Abrigo 6",
      cidade: "Cidade 6",
      bairro: "Bairro 6",
      rua: "Rua 6",
      numero: 600,
      complemento: "Complemento 6",
      tipoChavePix: "Telefone",
      chavePix: "1234567890",
      capacidade: EStatusCapacidade.Disponivel,
      precisaAjudante: false,
      precisaAlimento: true
  }
  ],
  quantidadeTotalRegistros: 7
};
