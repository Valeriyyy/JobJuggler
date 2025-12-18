export type Client = {
    id: string;
    guid: string;
    name: string;
    phone?: string;
    email?: string;
}

export type CompanyDTO = {
    id: string;
    name: string;
    mainContactName: string;
    mainContactEmail: string;
    mainContactPhone: string;
    dateCreated: Date;
}