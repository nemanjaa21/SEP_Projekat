import axios from "axios";

const authApi = axios.create({
    baseURL: process.env.REACT_APP_AUTH_URL,
    headers: {
        'Content-Type': 'application/json',
    },
});

const agencyApi = axios.create({
    baseURL: process.env.REACT_APP_AGENCY_URL,
    headers: {
        'Content-Type': 'application/json',
    },
});

const pspApi = axios.create({
    baseURL: process.env.REACT_APP_PSP_URL,
    headers: {
        'Content-Type': 'application/json',
    },
});

authApi.interceptors.request.use((config) => { 
    try{ 
        const token = sessionStorage.getItem('token');
        if(token){ 
            return {...config, headers: { 
                ...config.headers, 
                Authorization: `Bearer ${token}`, 
            }};
        } 
        return config; 
    } catch(e) { 
        console.log(e); 
        return Promise.reject(e); 
    } 
});

agencyApi.interceptors.request.use((config) => { 
    try{ 
        const token = sessionStorage.getItem('token');
        if(token){ 
            return {...config, headers: { 
                ...config.headers, 
                Authorization: `Bearer ${token}`, 
            }};
        } 
        return config; 
    } catch(e) { 
        console.log(e); 
        return Promise.reject(e); 
    } 
}); 

pspApi.interceptors.request.use((config) => { 
    try{ 
        const token = sessionStorage.getItem('token');
        if(token){ 
            return {...config, headers: { 
                ...config.headers, 
                Authorization: `Bearer ${token}`, 
            }};
        } 
        return config; 
    } catch(e) { 
        console.log(e); 
        return Promise.reject(e); 
    } 
}); 
 
export { authApi, agencyApi, pspApi };