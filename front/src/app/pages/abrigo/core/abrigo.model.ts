import { NamedEntity } from "../../../core/entities/named-entity";

export interface Abrigo extends NamedEntity {
  quantidadeNecessariaVoluntarios: number;
  capacidadeTotalPessoas: number;
  quantidadeVagasDisponiveis: number;
  alimentos: Alimento[];
  pessoasDesaparecidas: PessoaDesaparecida[];
  chavePix: string;
  tipoChavePix: string;
  endereco: Endereco;
  tipoAbrigo: ETipoDeAbrigo;
}
export enum ETipoDeAbrigo {
  Geral = 0,
  Animais = 1,
  Idosos = 2,
  Orfanato = 3
}

export interface Alimento {
  nome: string;
  quantidadeNecessaria: number;
}

export interface PessoaDesaparecida {
  nome: string;
  idade: number;
  informacaoAdicional: string;
}
export interface Endereco {
  rua: string;
  numero: string;
  bairro: string;
  cidade: string;
  cep: string;
  complemento: string;
}
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
  tipoAbrigo: ETipoDeAbrigo;

  capacidadeDesc?: string;
  precisaAjudanteDesc?: string;
  precisaAlimentoDesc?: string;

  capacidadeCssClass?: string;
  precisaAjudanteCssClass?: string;
  precisaAlimentoCssClass?: string;
  enderecoDesc?: string;
  ultimaAtualizacaoTxt?: string;
  tipoAbrigoDescricao?: ETipoDeAbrigo;
}

export interface AbrigosResult {
  abrigos: AbrigoPesquisa[];
  quantidadeTotalRegistros: number;
}
