import api from "../helpers/ConfigHelper";

export const getServiceOffer = async (id) => {
  return await api.get(`/get-service-offer/` + id);
};