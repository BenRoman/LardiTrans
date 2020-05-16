export type TransportationInfo = {
      transId: number;
      amountOfLikes: number;
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
export type Mutation = {
    updateLikes(id: number, amountOfLikes: number): String
  }