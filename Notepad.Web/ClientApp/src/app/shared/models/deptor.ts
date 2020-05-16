import { Debt } from './debt';

export interface Debtor {
    debts?: null | Array<Debt>;
    id?: null | string;
    name?: null | string;
    surname?: null | string;
    totalDebt?: number;
  }
