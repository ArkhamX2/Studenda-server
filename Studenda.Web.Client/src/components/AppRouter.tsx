
import { Routes, Route, Navigate } from 'react-router-dom';
import { privateRoutes, publicRoutes } from '../router';


const AppRouter = () => {
    
    return (           
           <Routes>
                {publicRoutes.map(route =>
                    <Route
                        Component={route.element}
                        path={route.path}
                        key={route.path} />
                )}
                <Route path="*" element={<Navigate to="/login" replace/>}/>
            </Routes>
    )
}

export default AppRouter