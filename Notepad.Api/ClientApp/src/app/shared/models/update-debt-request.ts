export interface UpdateDebtRequest {
    isRepaid: boolean;
    amount: number;
    creationDate?: string;
    description?: null | string;
    id: string;
    debtorId: string;
}
