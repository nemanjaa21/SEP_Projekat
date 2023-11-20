import axios from "axios";

const authApi = axios.create({
    baseURL: "https://localhost:7051/api",
    headers: {
        'Content-Type': 'application/json',
    },
});

const agencyApi = axios.create({
    baseURL: "https://localhost:7250/api",
    headers: {
        'Content-Type': 'application/json',
    },
});

const pspApi = axios.create({
    baseURL: "https://localhost:7288/",
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