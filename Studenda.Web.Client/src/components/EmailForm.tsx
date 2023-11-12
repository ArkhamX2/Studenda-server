import { FC } from 'react'
import LoginButton, { ButtonVariant } from './UI/button/LoginButton'
import LoginInput from './UI/imput/LoginInput'

const EmailForm: FC = () => {

    return (
        <div style={{position:'absolute', left:'40%', top:'30%', display:'flex',flexDirection:'column',border:'2px solid lightgray',padding:'50px'}}>
                <div style={{display:'flex', justifyContent:'center'}}>ваш.email@mail.com</div>
                <p style={{justifyContent:'stretch'}}>На почту был отправлен код из N цифр. <br/>Введите в поле ниже код из письма:</p>
                <LoginInput text='КОД'></LoginInput>
                <LoginButton variant={ButtonVariant.primary} text='Подтвердить'></LoginButton>
                <LoginButton variant={ButtonVariant.outlined} text='Получить код повторно'></LoginButton>
        </div>
    )
}

export default EmailForm