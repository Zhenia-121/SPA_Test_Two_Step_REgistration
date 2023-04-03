export interface Country {
  id: number;
  code: string;
  name: string;
}

export interface Province {
  id: number;
  countryId: number;
  name: string;
}
