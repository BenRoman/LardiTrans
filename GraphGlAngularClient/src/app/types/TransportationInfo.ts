export type TransportationInfo = {
      transId: number;
      loadingDate: string;
      vehicleType: string;
      cargoDescription: string;
      paymentType: string;
      routFrom: string;
      routTo: string;
      routFromCountry: string;
      routToCountry: string;
}

export type Query = {
    transportation: TransportationInfo[];
}