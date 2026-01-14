$(document).ready(function () {
    loadContacts();

    $('#contactsTableBody').on('click', 'tr', function () {
        var contact = $(this).data('contact');
        openModal(contact);
    });

    $('#btnAdd').click(function () {
        openModal(null);
    });

    $('#btnSave').click(function () {
        if (validateForm()) {
            saveContact();
        }
    });

    $('#btnDelete').click(function () {
        var id = $('#ContactId').val();
        if (confirm('Вы уверены, что хотите удалить этот контакт?')) {
            deleteContact(id);
        }
    });
});

function openModal(contact) {
    $('.error-msg').text('');
    $('#serverError').hide().text('');

    if (contact) {
        $('#ContactId').val(contact.id);
        $('#Name').val(contact.name);
        $('#MobilePhone').val(contact.mobilePhone);
        $('#JobTitle').val(contact.jobTitle);
        if (contact.birthDate) {
            $('#BirthDate').val(contact.birthDate.split('T')[0]);
        }

        $('#modalTitle').text('Редактирование контакта');
        $('#btnDelete').show();
    } else {
        $('#contactForm')[0].reset();
        $('#ContactId').val(0);
        $('#modalTitle').text('Новый контакт');
        $('#btnDelete').hide();
    }

    $('#contactModal').modal('show');
}

function loadContacts() {
    $.ajax({
        url: '/Contacts/GetContacts',
        type: 'GET',
        success: function (data) {
            var $tbody = $('#contactsTableBody');
            $tbody.empty();

            $.each(data, function (index, item) {
                var $tr = $('<tr>');

                $tr.data('contact', item);

                var displayDate = new Date(item.birthDate).toLocaleDateString('ru-RU');

                $('<td>').text(item.name).appendTo($tr);
                $('<td>').text(item.mobilePhone).appendTo($tr);
                $('<td>').text(item.jobTitle).appendTo($tr);
                $('<td>').text(displayDate).appendTo($tr);

                $tbody.append($tr);
            });
        },
        error: function () {
            alert('Ошибка при загрузке данных.');
        }
    });
}

function saveContact() {
    var contact = {
        Id: parseInt($('#ContactId').val()) || 0,
        Name: $('#Name').val(),
        MobilePhone: $('#MobilePhone').val(),
        JobTitle: $('#JobTitle').val(),
        BirthDate: $('#BirthDate').val()
    };

    $('#btnSave').prop('disabled', true);
    $('#serverError').hide();

    $.ajax({
        url: '/Contacts/SaveContact',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(contact),
        success: function () {
            $('#contactModal').modal('hide');
            loadContacts();
        },
        error: function (xhr) {
            var errorMessage = "Произошла неизвестная ошибка";

            if (xhr.responseText) {
                errorMessage = xhr.responseText;
            }
            else if (xhr.status === 400) {
                errorMessage = "Проверьте правильность введенных данных";
            }

            $('#serverError').text(errorMessage).show();
        },
        complete: function () {
            $('#btnSave').prop('disabled', false);
        }
    });
}

function deleteContact(id) {
    $.ajax({
        url: '/Contacts/DeleteContact?id=' + id,
        type: 'POST',
        success: function () {
            $('#contactModal').modal('hide');
            loadContacts();
        },
        error: function () {
            alert('Не удалось удалить запись.');
        }
    });
}

function validateForm() {
    var isValid = true;
    $('.error-msg').text('');

    var name = $('#Name').val().trim();
    if (name.length < 2) {
        $('#errorName').text('Имя слишком короткое (мин. 2 символа)');
        isValid = false;
    }

    if ($('#JobTitle').val().trim() === '') {
        $('#errorJob').text('Введите должность');
        isValid = false;
    }

    var phoneRegex = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;
    if (!phoneRegex.test($('#MobilePhone').val())) {
        $('#errorPhone').text('Некорректный формат телефона');
        isValid = false;
    }

    var dateStr = $('#BirthDate').val();
    if (!dateStr) {
        $('#errorDate').text('Дата рождения обязательна');
        isValid = false;
    } else {
        var inputDate = new Date(dateStr);
        var today = new Date();
        today.setHours(0, 0, 0, 0);

        if (inputDate > today) {
            $('#errorDate').text('Дата не может быть в будущем');
            isValid = false;
        }
        if (inputDate.getFullYear() < 1900) {
            $('#errorDate').text('Год рождения должен быть не ранее 1900');
            isValid = false;
        }
    }

    return isValid;
}