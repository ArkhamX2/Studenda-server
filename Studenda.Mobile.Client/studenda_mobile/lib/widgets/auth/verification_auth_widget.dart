import 'package:flutter/material.dart';
import 'package:studenda_mobile/resources/colors.dart';
import 'package:studenda_mobile/widgets/UI/button_widget.dart';

class VerificationAuthWidget extends StatefulWidget {
  final String email;
  const VerificationAuthWidget({super.key, required this.email});

  @override
  State<VerificationAuthWidget> createState() => _VerificationAuthWidgetState();
}

class _VerificationAuthWidgetState extends State<VerificationAuthWidget> {
  final controller = TextEditingController();

  final formKey = GlobalKey<FormState>();
  
  @override
  void dispose() {
    controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromARGB(255, 240, 241, 245),
      appBar: AppBar(
        leading: IconButton(
          icon: const Icon(
            Icons.chevron_left_sharp,
            color: Colors.white,
          ),
          onPressed: () => {Navigator.of(context).pop()},
        ),
        titleSpacing: 0,
        centerTitle: true,
        title: const Text(
          'Вход',
          style: TextStyle(color: Colors.white, fontSize: 25),
        ),
      ),
      body: Container(
        alignment: AlignmentDirectional.center,
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 17),
          child: Form(
            key: formKey,
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Text(
                  widget.email,
                  style: const TextStyle(
                    color: mainForegroundColor,
                    fontSize: 20,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(
                  height: 32,
                ),
                const Text(
                  "На почту был отправлен код из N цифр. Введите в поле ниже код из письма:",
                  textAlign: TextAlign.center,
                  style: TextStyle(
                    color: mainForegroundColor,
                    fontSize: 18,
                  ),
                ),
                const SizedBox(
                  height: 17,
                ),
                TextFormField(
                  controller: controller,
                  decoration: InputDecoration(
                    border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(5),),
                  ),
                  keyboardType: TextInputType.emailAddress,
                  autofillHints: const [AutofillHints.email],
                  validator: (code) =>
                      code != null && code.length != 5 ? "Неверный код" : null,
                ),
                const SizedBox(
                  height: 5,
                ),
                //TODO: Сделать каунтдаун на 2 минуты
                const Text(
                  "Повторно код можно получить через",
                  style: TextStyle(
                    color: Color.fromARGB(160, 101, 59, 159),
                    fontSize: 20,
                  ),
                ),
                const SizedBox(
                  height: 23,
                ),
            
                StudendaButton(title: "Получить код повторно", event: () {
                  //Повторный запрос с емейлом
                },),
                const SizedBox(
                  height: 17,
                ),
                StudendaButton(title: "Подтвердить", event: () {
                  final form = formKey.currentState!;
                  if(form.validate()){
                    final code = controller.text;

                    //TODO: Отправить код на сервер

                    Navigator.of(context).pushReplacementNamed('/main_nav');
                  }
            
                },),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
