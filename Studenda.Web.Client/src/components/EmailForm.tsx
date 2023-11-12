import { FC } from 'react'
import LoginButton, { ButtonVariant } from './UI/button/LoginButton'
import LoginInput, { TextAlign } from './UI/imput/LoginInput'
import LoginLabel from './UI/label/LoginLabel'

const EmailForm: FC = () => {

    return (
        <div style={{position:'absolute', left:'40%', top:'30%', display:'flex',flexDirection:'column',border:'2px solid lightgray',padding:'50px'}}>
                <LoginLabel text='ваш.email@mail.com' align={TextAlign.center}></LoginLabel>
                <div style={{display:'flex', flexDirection:'column', alignSelf:'center'}}>
                    <LoginLabel text='На почту был отправлен код из N цифр.'></LoginLabel>
                    <LoginLabel text='Введите в поле ниже код из письма:'></LoginLabel>
                </div>
                
                <LoginInput text='КОД' align={TextAlign.center}></LoginInput>
                <LoginButton variant={ButtonVariant.primary} text='Подтвердить'></LoginButton>
                <LoginButton variant={ButtonVariant.outlined} text='Получить код повторно'></LoginButton>
        </div>
    )
}

export default EmailForm