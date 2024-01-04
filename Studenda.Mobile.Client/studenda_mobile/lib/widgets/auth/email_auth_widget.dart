import 'package:flutter/material.dart';
import 'package:studenda_mobile/resourses/colors.dart';
import 'package:studenda_mobile/widgets/UI/button_widget.dart';

class EmailAuthWidget extends StatefulWidget {
  const EmailAuthWidget({super.key});

  @override
  State<EmailAuthWidget> createState() => _EmailAuthWidgetState();
}

class _EmailAuthWidgetState extends State<EmailAuthWidget> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: mainBackgroundColor,
      appBar: AppBar(
        leading: IconButton(
          icon: const Icon(Icons.chevron_left_sharp),
          onPressed: () => {},
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
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              const Text(
                "Введите свой email:",
                style: TextStyle(
                    color: mainForegroundColor, fontSize: 20,),
              ),
              const SizedBox(
                height: 23,
              ),
              const TextField(),
              const SizedBox(
                height: 23,
              ),
              Center(
                child: StudendaButton(title: "Подтвердить", event: () {}),
                ),
              const SizedBox(
                height: 34,
              ),
            ],
          ),
        ),
      ),
    );
  }
}
