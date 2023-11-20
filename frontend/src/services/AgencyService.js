import { agencyApi } from "../helpers/ConfigHelper";

export const getAllServiceOfferItem = async () => {
  return await agencyApi.get(`/ServiceOfferItem/get-all-service-offer-item`);
};

export const createServiceOffer = async (serviceOffer) => {
  return await agencyApi.post(`/ServiceOffer/`, serviceOffer);
};