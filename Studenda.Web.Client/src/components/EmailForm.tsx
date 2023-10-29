import { FC } from 'react'

const EmailForm: FC = () => {

    return (
        <div style={{position:'absolute', left:'40%', top:'30%', display:'flex',flexDirection:'column',border:'2px solid lightgray',padding:'50px'}}>
                <div style={{display:'flex', justifyContent:'center'}}>ваш.email@mail.com</div>
                <p style={{justifyContent:'stretch'}}>На почту был отправлен код из N цифр. <br/>Введите в поле ниже код из письма:</p>
                <input style={{display:'flex'}} placeholder='КОД'></input>
                <button style={{display:'flex'}} >Подтвердить</button>
                <button style={{display:'flex'}} >Получить код повторно</button>
        </div>
    )
}

export default EmailForm