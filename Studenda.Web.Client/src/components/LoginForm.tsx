import { FC } from 'react'

const LoginForm: FC = () => {

    return (
        <div style={{position:'absolute', left:'40%', top:'30%', display:'flex',flexDirection:'column',border:'2px solid lightgray',padding:'50px'}}>
                <p style={{display:'flex'}}>Введите свой email</p>
                <input style={{display:'flex'}}></input>
                <button style={{display:'flex'}} >Получить код</button>
                
        </div>

    )
}

export default LoginForm