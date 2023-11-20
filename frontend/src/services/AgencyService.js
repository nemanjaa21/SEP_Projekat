import api from "../helpers/ConfigHelper";

export const getAllServiceOfferItem = async () => {
  return await api.get(`/get-all-service-offer-item`);
};