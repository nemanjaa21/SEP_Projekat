import { agencyApi } from "../helpers/ConfigHelper";

export const getAllServiceOfferItem = async () => {
  return await agencyApi.get(`/ServiceOfferItem/get-all-service-offer-item`);
};

export const getServiceOfferById = async (id) => {
  return await agencyApi.get(`/ServiceOffer/` + id);
}

export const createServiceOffer = async (serviceOffer) => {
  return await agencyApi.post(`/ServiceOffer/`, serviceOffer);
};

export const getAllPaymentServices = async (agencyId) => {
  return await agencyApi.get(`/PaymentServices`, agencyId);
}

export const subscribeOnPaymentServices = async (paymentServies) => {
  return await agencyApi.post(`/PaymentServices`, paymentServies);
}