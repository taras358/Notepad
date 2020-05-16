import { DebtResponse } from './debt-response.model';

export interface DebtorResponse {
  debts?: null | Array<DebtResponse>;
  id?: null | string;
  name?: null | string;
  surname?: null | string;
  totalDebt?: number;
}
